using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Helpers;
using System.Web.Mvc;
using SuperHeroi.MVC.Models;

namespace SuperHeroi.MVC.Controllers
{
    public class UploadsController : Controller
    {
        public string DiretorioBase = "~/Upload/";
        public string DiretorioTemp = "~/Upload/Temp/";
        public string DiretorioCrop = "~/Upload/Crop/";

        [Authorize]
        public ActionResult Index()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var idUsuario = claimsIdentity.FindFirst(c => c.Type == "ClaimIdUsuario").Value;

                var dirUsuario = idUsuario + "/";
                var dir = Server.MapPath(DiretorioCrop + dirUsuario);
                if (Directory.Exists(dir))
                {
                    ViewBag.Arquivos = ListDiretory(dir);
                    ViewBag.Dir = DiretorioCrop.Replace("~", "..") + dirUsuario;
                }
            }

            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(object fileNameIdUsuario)
        {
            if (fileNameIdUsuario == null) throw new ArgumentNullException("fileNameIdUsuario");

            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                fileNameIdUsuario = claimsIdentity.FindFirst(c => c.Type == "ClaimIdUsuario").Value;

                //********************************************************/
                //Realiza o upload da imagem para um diretório temporário
                //para depois poder ser editada (recortada) e colocada no
                //local correto, depois a imagem temporário é liberada
                //********************************************************/

                //Captura a imagem
                var image = WebImage.GetImageFromRequest();

                //Verifica se a imagem foi capturada
                if (image != null)
                {
                    //Verifica o tamanho dela, se for maior que o determinado, reduz proporcionalmente
                    if (image.Width > 1110)
                    {
                        image.Resize(1110, ((1110 * image.Height) / image.Width));
                    }

                    //Nome do arquivo a ser salvo
                    var filename = fileNameIdUsuario + Path.GetExtension(image.FileName);

                    /**********/
                    //Diretório
                    /**********/
                    var dir = Server.MapPath(DiretorioTemp);
                    //verifica se o diretório já existe,
                    if (!Directory.Exists(dir))
                    {
                        //se o diretório não existe cria
                        Directory.CreateDirectory(dir);
                    }
                    else
                    {
                        //se o diretório existe, remove e cria novament
                        //(se assegura que não haverá outro arquivo no diretório temporário)
                        Directory.Delete(dir, true);
                        Directory.CreateDirectory(dir);
                    }


                    /**********/
                    //Arquivo
                    /**********/
                    image.Save(Path.Combine(dir, filename));
                    ViewBag.Filename = DiretorioTemp.Replace("~", "..") + filename;

                    return View();
                }
            }

            return View();
        }

        [HttpPost]
        public virtual ActionResult CropImage(string imagePath, int? cropPointX, int? cropPointY, int? imageCropWidth, int? imageCropHeight)
        {
            if (string.IsNullOrEmpty(imagePath)
                || !cropPointX.HasValue
                || !cropPointY.HasValue
                || !imageCropWidth.HasValue
                || !imageCropHeight.HasValue)
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
            }

            //Arquivo
            var imageBytes = System.IO.File.ReadAllBytes(Server.MapPath(imagePath));
            //Imagem editada (recortada)
            var croppedImage = ImageHelper.CropImage(imageBytes, cropPointX.Value, cropPointY.Value, imageCropWidth.Value, imageCropHeight.Value);
            //Nome do arquivo
            var fileName = Path.GetFileNameWithoutExtension(imagePath);
            /**********/
            //Diretório
            /**********/
            var dir = Server.MapPath(DiretorioCrop + fileName + "/");
            //path é o caminho da imagem cortada
            var path = "";
            //verifica se o diretório já existe,
            if (!Directory.Exists(dir))
            {
                //se o diretório não existe cria
                Directory.CreateDirectory(dir);
                var nomeFile = fileName + ".1" + Path.GetExtension(imagePath);

                /**********/
                //Arquivo
                /**********/
                FileHelper.SaveFile(croppedImage, Path.Combine(dir, nomeFile));

                //Exclui temporário
                var dirTemp = Server.MapPath(DiretorioTemp);
                //verifica se o diretório ainda existe, e remove
                if (Directory.Exists(dirTemp))
                {
                    Directory.Delete(dirTemp, true);
                }

                path = DiretorioCrop + fileName + "/" + nomeFile;
            }
            else
            {
                for (int i = 1; i < 11; i++)
                {
                    var nomeFile = fileName + "." + i + Path.GetExtension(imagePath);

                    if (!System.IO.File.Exists(dir + nomeFile))
                    {
                        /**********/
                        //Arquivo
                        /**********/
                        FileHelper.SaveFile(croppedImage, Path.Combine(dir, nomeFile));

                        //Exclui temporário
                        var dirTemp = Server.MapPath(DiretorioTemp);
                        //verifica se o diretório ainda existe, e remove
                        if (Directory.Exists(dirTemp))
                        {
                            Directory.Delete(dirTemp, true);
                        }

                        path = DiretorioCrop + fileName + "/" + nomeFile;

                        break;
                    }
                }
            }

            return Json(new { photoPath = path.Replace("~", "..") }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string fileName)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var idUsuario = claimsIdentity.FindFirst(c => c.Type == "ClaimIdUsuario").Value;

                var dir = Server.MapPath(DiretorioCrop + idUsuario);
                var image = Path.Combine(dir, fileName);

                if (System.IO.File.Exists(image))
                {
                    System.IO.File.Delete(image);
                }

                // se não houver arquivos no diretório, remove-o
                if (!ListDiretory(dir).Any())
                {
                    Directory.Delete(dir, true);
                }
            }

            return RedirectToAction("Index");
        }

        public FileInfo[] ListDiretory(string dir)
        {
            //Marca o diretório a ser listado
            var diretorio = new DirectoryInfo(dir);

            //Executa função GetFile(Lista os arquivos desejados de acordo com o parametro)
            var arquivos = diretorio.GetFiles("*.*");

            return arquivos;
        }
    }
}