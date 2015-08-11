using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Mvc;
using SuperHeroi.Application.Interfaces;
using SuperHeroi.Application.ViewModels;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Constants.PreApproval;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Service;

namespace SuperHeroi.MVC.Controllers
{
    public class PagamentoController : Controller, IDisposable
    {
        private readonly IPedidoAppService _pedidoAppService;
        private readonly INotificacaoAppService _notificacaoAppService;
        private readonly bool _isSandbox;

        public PagamentoController(
            IPedidoAppService pedidoAppService,
            INotificacaoAppService notificacaoAppService)
        {
            _pedidoAppService = pedidoAppService;
            _notificacaoAppService = notificacaoAppService;
            //True: Ambiente de teste / False: Ambiente de produção
            _isSandbox = true;
        }

        // GET: Pagamento
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Assinar(int planoId)
        {
            var nomePlano = "";
            var valorPlano = Convert.ToDecimal(1.90m);

            switch (planoId)
            {
                case 1:
                    {
                        nomePlano = "Bronze";
                        break;
                    }
                case 2:
                    {
                        nomePlano = "Prata";
                        break;
                    }
                case 3:
                    {
                        nomePlano = "Ouro";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            string idUsuario = null;
            string cpf = "12925506780";
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                idUsuario = claimsIdentity.FindFirst(c => c.Type == "ClaimIdHeroi").Value;
            }

            if (idUsuario == null)
                return RedirectToAction("Index");

            var codigoReferencia = "REF" + idUsuario;


            string msg = "Sucesso!";
            bool isSandbox = _isSandbox;
            EnvironmentConfiguration.ChangeEnvironment(isSandbox);
            AccountCredentials credentials = PagSeguroConfiguration.Credentials(isSandbox);

            try
            {
                PreApprovalRequest payment = new PreApprovalRequest();
                payment.Currency = Currency.Brl;

                payment.Reference = codigoReferencia;

                payment.Sender = new Sender(
                    "Vinicius Silva",
                    "vinicius.buffolo@hotmail.com",
                    new Phone("28", "999404678")
                );

                //PAGAMENTO RECORRENTE
                var now = DateTime.Now;
                payment.PreApproval = new PreApproval();
                // Indica se a assinatura será gerenciada pelo PagSeguro (auto) ou pelo Vendedor (manual).
                payment.PreApproval.Charge = Charge.Auto;
                // Nome/Identificador da assinatura (Nome do plano)
                payment.PreApproval.Name = nomePlano;
                // Valor exato de cada cobrança
                payment.PreApproval.AmountPerPayment = valorPlano;
                // Valor máximo que pode ser cobrado por mês de vigência da assinatura, independente de sua periodicidade.
                payment.PreApproval.MaxAmountPerPeriod = valorPlano;
                // Periodicidade da cobrança.
                payment.PreApproval.Period = "Monthly";
                //
                payment.PreApproval.DayOfMonth = now.Day;
                // Início da vigência da assinatura
                payment.PreApproval.InitialDate = now.AddDays(1);
                // Fim da vigência da assinatura
                payment.PreApproval.FinalDate = now.AddMonths(12);
                //
                payment.PreApproval.MaxPaymentsPerPeriod = 1;
                // Valor máximo que pode ser cobrado durante a vigência da assinatura.
                payment.PreApproval.MaxTotalAmount = valorPlano * 12;
                // Detalhes/Descrição da assinatura
                payment.PreApproval.Details =
                    string.Format("Todo mês {0} será cobrado o valor de {1} referente ao plano escolhido.", now.Day, payment.PreApproval.AmountPerPayment.ToString("C2"));


                // Sets the url used by PagSeguro for redirect user after ends checkout process
                payment.RedirectUri = new Uri("http://superheroi.azurewebsites.net/Pagamento/Retorno/");

                // Sets the url used for user review the signature or read the rules
                payment.ReviewUri = new Uri("http://superheroi.azurewebsites.net/");

                SenderDocument senderCPF = new SenderDocument(Documents.GetDocumentByType("CPF"), cpf);
                payment.Sender.Documents.Add(senderCPF);

                Uri paymentRedirectUri = PreApprovalService.CreateCheckoutRequest(credentials, payment);


                var pedido = new PedidoViewModel()
                {
                    UsuarioId = new Guid(idUsuario),
                    CodRef = codigoReferencia,
                    DataAlteracao = DateTime.Now
                };
                _pedidoAppService.Add(pedido);


                return Redirect(paymentRedirectUri.ToString());
            }
            catch (PagSeguroServiceException exception)
            {
                if (exception.StatusCode == HttpStatusCode.Unauthorized)
                {
                    msg = "Não autorizado: verifique se as credenciais usadas na chamada de serviço web estão corretas. \n";
                }
                else
                {
                    msg = "Error. " + exception.Message;
                }
            }

            TempData["Message"] = msg;
            return RedirectToAction("Index");
        }

        // POST: PagSeguro/Notificacao
        [HttpPost]
        public ContentResult Notificacao(FormCollection collection)
        {
            HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "https://sandbox.pagseguro.uol.com.br");
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "https://sandbox.pagseguro.uol.com.br");

            string dtNow = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");

            if (!String.IsNullOrEmpty(collection["notificationType"]))
            {
                string notificationType = collection["notificationType"];
                string notificationCode = collection["notificationCode"];

                try
                {
                    bool isSandbox = _isSandbox;
                    EnvironmentConfiguration.ChangeEnvironment(isSandbox);
                    AccountCredentials credentials = PagSeguroConfiguration.Credentials(isSandbox);

                    if (notificationType.Trim().Equals("transaction"))
                    {
                        try
                        {
                            // obtendo o objeto transaction a partir do código de notificação  
                            Transaction transaction = NotificationService.CheckTransaction(
                                credentials,
                                notificationCode,
                                false
                            );

                            int status = transaction.TransactionStatus;

                            using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/MVC_LogPagSeguro_" + dtNow + ".txt"), true))
                            {
                                _testData.WriteLine("Log de Notificações"); // Write the file.
                                _testData.WriteLine("Código de Notificação: " + notificationCode);
                                _testData.WriteLine("Código da transação: " + transaction.Code);
                                _testData.WriteLine("Status da transação: " + GetDescricaoStatus(status));
                                _testData.WriteLine("Código da referência: " + transaction.Reference);
                                _testData.WriteLine("Tipo da transação: " + transaction.TransactionType.ToString());
                                _testData.WriteLine("Tipo notificação: " + notificationType);
                            }

                            // O que fazer?
                            // Atualiza na base de dados o status da transação com base nos dados fornecidos pela transação

                            var pedido =
                                _pedidoAppService.GetAll().First(x => x.CodRef == transaction.Reference);

                            if (pedido != null)
                            {
                                pedido.CodTransacao = transaction.Code;
                            }
                            _pedidoAppService.Update(pedido);

                            var notificacao = _notificacaoAppService.GetAll().FirstOrDefault(x => x.CodRef == transaction.Reference);
                            if (notificacao != null)
                            {
                                notificacao.CodNotificacao = notificationCode;
                                notificacao.CodTransacao = transaction.Code;
                                notificacao.Status = GetDescricaoStatus(status);
                                notificacao.TipoNotificacao = notificationType;
                                _notificacaoAppService.Update(notificacao);
                            }
                            else
                            {
                                notificacao = new NotificacaoViewModel()
                                {
                                    PedidoId = pedido.PedidoId,
                                    CodNotificacao = notificationCode,
                                    CodTransacao = transaction.Code,
                                    Status = GetDescricaoStatus(status),
                                    TipoNotificacao = notificationType,
                                    CodRef = pedido.CodRef
                                };
                                _notificacaoAppService.Add(notificacao);
                            }
                        }
                        catch (PagSeguroServiceException exception)
                        {
                            using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/MVC_Error_" + dtNow + ".txt"), true))
                            {
                                _testData.WriteLine("Error:"); // Write the file.
                                _testData.WriteLine(exception.StatusCode);
                                _testData.WriteLine("-Try- transaction");
                            }
                        }
                    }
                    else if (notificationType.Trim().Equals("preApproval"))
                    {
                        // Transação Tipo Assinatura
                        var preApprovalTransaction = NotificationService.CheckTransaction(credentials, notificationCode, true);
                        using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/MVC_LogPagSeguro_" + dtNow + ".txt"), true))
                        {
                            _testData.WriteLine("Log de Notificações"); // Write the file.
                            _testData.WriteLine("Código de Notificação: " + notificationCode);
                            _testData.WriteLine("Código da assinatura: " + preApprovalTransaction.Code);
                            _testData.WriteLine("Status da assinatura: " + preApprovalTransaction.Status);
                            _testData.WriteLine("Código da referência: " + preApprovalTransaction.Reference);
                            _testData.WriteLine("Tipo da transação: " + preApprovalTransaction.TransactionType.ToString());
                            _testData.WriteLine("Tipo notificação: " + notificationType);
                        }

                        // O que fazer?
                        // Atualiza na base de dados o status da transação com base nos dados fornecidos pela transação

                        var notificacao = new NotificacaoViewModel
                        {
                            CodNotificacao = notificationCode,
                            CodAssinatura = preApprovalTransaction.Code,
                            Status = preApprovalTransaction.Status,
                            CodRef = preApprovalTransaction.Reference,
                            TipoNotificacao = notificationType
                        };
                        _notificacaoAppService.Add(notificacao);

                        //var pedido = 
                        //    _pedidoAppService.GetAll().FirstOrDefault(x => x.CodRef == preApprovalTransaction.Reference);
                        //if (pedido != null) pedido.CodAssinatura = preApprovalTransaction.Code;
                        //_pedidoAppService.Update(pedido);
                    }
                }
                catch (Exception e1)
                {
                    using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/MVC_ErrorNotificacao_" + dtNow + ".txt"), true))
                    {
                        _testData.WriteLine("ErrorNotificacao"); // Write the file.
                        _testData.WriteLine("Error: " + e1.Message);
                        _testData.WriteLine("-Try-");
                    }
                }
            }

            return Content("");
        }

        [HttpPost]
        public ActionResult CancelarAssinatura(FormCollection collection)
        {
            string dtNow = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");
            string msg = "Sucesso!";

            try
            {
                bool isSandbox = _isSandbox;
                EnvironmentConfiguration.ChangeEnvironment(isSandbox);
                AccountCredentials credentials = PagSeguroConfiguration.Credentials(isSandbox);
                string Token = credentials.Token;
                string Email = credentials.Email;
                string preApprovalCode = collection["txtPreApprovalCodeByVerificar"];

                var cancelResult = PreApprovalService
                                    .CancelPreApproval(credentials, preApprovalCode);

                if (cancelResult)
                {
                    //Grava no banco ou faz alguma outra ação
                }

                TempData["Message"] = msg;
            }
            catch (PagSeguroServiceException exception)
            {
                msg = "Error. " + exception.Message;
                TempData["Message"] = msg;

                using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/MVC_Cancelar_Error_" + dtNow + ".txt"), true))
                {
                    _testData.WriteLine("Error:"); // Write the file.
                    _testData.WriteLine(exception.Message);
                }

                //foreach (ServiceError error in exception.Errors)
                //{
                //    Console.WriteLine(error);
                //}
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult VerificarPreApproval(FormCollection collection)
        {
            string TransacaoCode = collection["txtTransacaoByVerificar"];
            if (VerificarSeExistePreApproval(TransacaoCode))
            {
                TempData["Message"] = "Sucesso";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = "Não localizado. Favor checar log gerado.";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Retorno(string code)
        {
            string dtNow = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");
            if (Request.HttpMethod == "POST")
            {
                //o método POST indica que a requisição é o retorno da validação NPI.
                using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/MVC_Retorno_POST_" + dtNow + ".txt"), true))
                {
                    _testData.WriteLine("MVC_Retorno_POST_"); // Write the file.
                    _testData.WriteLine("code: " + code);
                }

                return View();
            }
            else if (Request.HttpMethod == "GET")
            {
                //o método GET indica que a requisição é o retorno do Checkout PagSeguro para o site vendedor.
                //no término do checkout o usuário é redirecionado para este bloco.
                using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/MVC_Retorno_GET_" + dtNow + ".txt"), true))
                {
                    _testData.WriteLine("MVC_Retorno_GET_"); // Write the file.
                    _testData.WriteLine("code: " + code);
                }
            }
            return View();
        }

        protected String GetDescricaoStatus(int idStatus)
        {
            string resultado = String.Empty;

            switch (idStatus)
            {
                case 1:
                    resultado = "Aguardando pagamento";
                    break;
                case 2:
                    resultado = "Em análise";
                    break;
                case 3:
                    resultado = "Paga";
                    break;
                case 4:
                    resultado = "Disponível";
                    break;
                case 5:
                    resultado = "Em disputa";
                    break;
                case 6:
                    resultado = "Devolvida";
                    break;
                case 7:
                    resultado = "Cancelada";
                    break;
                default:
                    resultado = idStatus.ToString();
                    break;
            }
            return resultado;
        }

        protected bool VerificarSeExistePreApproval(string TransacaoCode)
        {
            //Este método pode ser alterado para retornar uma string também.
            //Neste caso você pode retornar uma string com as mensagens de erro.
            //Ou deixar que o usuário veja apenas uma mensagem padrao e o erro é gravado no banco ou enviado por e-mail pro ADM.

            string dtNow = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");
            try
            {
                bool isSandbox = _isSandbox;
                EnvironmentConfiguration.ChangeEnvironment(isSandbox);
                AccountCredentials credentials = PagSeguroConfiguration.Credentials(isSandbox);

                string Token = credentials.Token;
                string Email = credentials.Email;

                // Transação Tipo Assinatura
                var preApprovalTransaction = NotificationService
                                                .CheckTransaction(credentials, TransacaoCode, true);

                return true;
            }
            catch (Exception exception)
            {
                using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/MVC_Verificar_Error_" + dtNow + ".txt"), true))
                {
                    _testData.WriteLine("Error:"); // Write the file.
                    _testData.WriteLine(exception.Message);
                }
            }

            return false;
        }

        void IDisposable.Dispose()
        {
            _pedidoAppService.Dispose();
            _notificacaoAppService.Dispose();
        }
    }
}