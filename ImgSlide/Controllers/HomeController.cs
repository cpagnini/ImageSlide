using ImgSlide.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImgSlide.Controllers
{
  [OutputCache(NoStore = true, Duration = 0)]
  public class HomeController : Controller
    {
    
    public ActionResult Index(string sede="Default")
    {
 
            

        string[] folderPath = System.Configuration.ConfigurationManager.AppSettings["ImgPath"].Split(',');
        string currentPath = string.Empty;
        foreach (string path in folderPath)
        {
            FileInfo filePath = new FileInfo(path);
            if(filePath.Name != sede)
            {
            continue;
            }
            else
            {
            currentPath = path;
            break;
            }
               
        }
        int SlideTime = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["SlideTime"])*1000; //SlideTime is a configurable parameter that allow user to specify the time between images
        List<ImageModel> imgList = new List<ImageModel>(); //Model primary has a path, hence we need a list of
        var listFiles = Directory.EnumerateFiles(Server.MapPath(currentPath), "*.*", SearchOption.AllDirectories) //Takes all file with given extension
        .Where(s => s.EndsWith(".png") || s.EndsWith(".jpg"));
        
        if (listFiles.Count() > 0)
        {

            foreach (var file in listFiles)
            {
                //Creates Model's objects
                ImageModel img = new ImageModel();
                var fileSplit = file.Split('\\');
                var imageName = fileSplit[fileSplit.Length - 1];
                img.subFolder = sede;
                img.imageName = imageName;
                img.SlideTime = SlideTime;
                imgList.Add(img);

            }
        }

        if(listFiles.Count() == 0)
        {
            return RedirectToAction("DefaultIndex");
        }
            string totalTimeAfterRefresh = ((SlideTime / 1000) * imgList.Count()).ToString();
            Response.AddHeader("Refresh", totalTimeAfterRefresh); //Add header to specify when the page have to be refreshed-->Number of images*time (seconds)

            return View(imgList);
    }

        public ActionResult DefaultIndex()
        {
            

            return View();
        }

       

    }

    
}


/*
using ImgSlide.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImgSlide.Controllers
{
  [OutputCache(NoStore = true, Duration = 0)]
  public class HomeController : Controller
    {
    
    public ActionResult Index(string sede="Default")
    {
 
            

        string[] folderPath = System.Configuration.ConfigurationManager.AppSettings["ImgPath"].Split(',');
        string currentPath = string.Empty;
        foreach (string path in folderPath)
        {
            FileInfo filePath = new FileInfo(path);
            if(filePath.Name != sede)
            {
            continue;
            }
            else
            {
            currentPath = path;
            break;
            }
               
        }
        int SlideTime = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["SlideTime"])*1000; //SlideTime is a configurable parameter that allow user to specify the time between images
        List<ImageModel> imgList = new List<ImageModel>(); //Model primary has a path, hence we need a list of
        var listFiles = Directory.EnumerateFiles(Server.MapPath(currentPath), "*.*", SearchOption.AllDirectories) //Takes all file with given extension
        .Where(s => s.EndsWith(".png") || s.EndsWith(".jpg"));
        ImageModel img2 = new ImageModel();
        if (listFiles.Count() > 0)
        {
            if (Session["ID"] == null)
            {
                Session["ID"] = 1;
            }
            else
            {
                Session["ID"] = ((Convert.ToInt32(Session["ID"]) + 1) % listFiles.Count()) +1;
            }
            
            img2.imageName = "0"+Session["ID"].ToString() + ".png";
            img2.subFolder = sede;

            //foreach (var file in listFiles)
            //{
            //    //Creates Model's objects
            //    ImageModel img = new ImageModel();
            //    var fileSplit = file.Split('\\');
            //    var imageName = fileSplit[fileSplit.Length - 1];
            //    img.subFolder = sede;
            //    img.imageName = imageName;
            //    img.SlideTime = SlideTime;
            //    imgList.Add( img);

            //}
        }

        if(listFiles.Count() == 0)
        {
            return RedirectToAction("DefaultIndex");
        }
        //string totalTimeAfterRefresh = ((SlideTime / 100) * imgList.Count()).ToString();
        //Response.AddHeader("Refresh", totalTimeAfterRefresh); //Add header to specify when the page have to be refreshed-->Number of images*time (seconds)
        
        return View(img2);
    }

        public ActionResult DefaultIndex()
        {
            

            return View();
        }

       

    }

    
}
  */
