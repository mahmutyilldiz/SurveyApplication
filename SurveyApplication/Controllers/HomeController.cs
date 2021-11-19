using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SurveyApplication.Models;
using SurveyApplication.Models.AppDBContext;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SurveyApplication.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDBContext _db;
        public HomeController(AppDBContext db)
        {
            _db = db;
        }

        private Guid _surveyID;
        private Survey _mySurvey;

        public ActionResult Index()
        {
            if (_mySurvey == null) _mySurvey = new Survey();
            return View(_mySurvey);
        }

        public ActionResult createQuestion(Guid SurveyId)
        {
            Survey survey = _db.Survey.Where(w => w.SurveyId == SurveyId).FirstOrDefault();
            if (survey == null)
            {
                survey = new Survey();

                //var url = $"{AppDBContext.Request.Scheme}://{AppDBContext.Request.Host}{AppDBContext.Request.Path}";
                //var link = $"<a href={url}</a>";

                survey.Link = "http://8082/Home/ListofQustion";
                survey.SurveyId = SurveyId;
                _db.Survey.Add(survey);
                _db.SaveChanges();
            }

            ViewBag.SurveyId = SurveyId;
            _surveyID = SurveyId;
            return View();
        }

        [HttpPost]
        public ActionResult createQuestion(Questions questions)
        {
            Questions questionsFind = _db.Question.Where(w => w.Name == questions.Name && w.SurveyId == questions.SurveyId).FirstOrDefault();
            if (questionsFind == null)
            {
                _db.Question.Add(questions);
                _db.SaveChanges();
            }
            else
            {
                //hata fırlat
            }
            return RedirectToAction("createOption", new { QuestionId = questions.QuestionId });
        }

        public ActionResult createOption(Guid QuestionId)
        {
            ViewBag.QuestionId = QuestionId;
            return View();
        }

        [HttpPost]
        public ActionResult createOption(Options option)
        {
            Options optionFind = _db.Option.Where(w => w.Name == option.Name && w.QuestionId == option.QuestionId).FirstOrDefault();
            if (optionFind == null)
            {
                _db.Option.Add(option);
                _db.SaveChanges();
            }
            else
            {
                //hata fırlat
            }
            return RedirectToAction("QuestionDetails", new { QuestionId = option.QuestionId });
        }
        public ActionResult QuestionDetails(Guid QuestionId)
        {
            Questions questions = _db.Question.Where(w => w.QuestionId == QuestionId).FirstOrDefault();
            List<Options> listOption = _db.Option.Where(w => w.QuestionId == QuestionId).ToList();

            //
            if (questions.ListOption == null)
            {
                questions.ListOption = new List<Options>();
            }
            questions.ListOption = listOption;

            return View(questions);
        }

        public ActionResult ListofQuestion(Guid SurveyId)
        {
            List<Questions> listQuestions = _db.Question.Where(w=>w.SurveyId== SurveyId).ToList();
            foreach (Questions item in listQuestions)
            {
                List<Options> lsOp = _db.Option.Where(w => w.QuestionId == item.QuestionId).ToList();
                item.ListOption = lsOp;

            }
            return View(listQuestions);
        }
        [HttpPost]
        public ActionResult SaveResult(IFormCollection collection)
        {
            
            foreach (var item in collection.Keys)
            {
                Results results = new Results();

            }
            return View();
        }
        //Mail Gonderme 
        public IActionResult Contact()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Contact(Mail m)  //Mail sınıfından m diye bir değişken tanımlarız
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Burası aynı kalacak
                client.Credentials = new NetworkCredential("yildiz.mahmut7534@gmail.com", "ardahan75");
                client.EnableSsl = true;
                MailMessage msj = new MailMessage(); //Yeni bir MailMesajı oluşturuyoruz
                msj.From = new MailAddress(m.Email, m.Adi + " " + m.Soyadi); //iletişim kısmında girilecek mail buaraya gelecektir
                msj.To.Add("yildiz.mahmut7534@gmail.com"); //Buraya kendi mail adresimizi yazıyoruz
                msj.Subject = m.Konu + "" + m.Email; //Buraya iletişim sayfasında gelecek konu ve mail adresini mail içeriğine yazacaktır
                msj.Body = m.Mesaj; //Mail içeriği burada aktarılacakır
                client.Send(msj); //Clien sent kısmında gönderme işlemi gerçeklecektir.
                //Bu kısımdan itibaren sizden kullanıcıya gidecek mail bilgisidir 
                MailMessage msj1 = new MailMessage();
                msj1.From = new MailAddress("yildiz.mahmut7534@gmail.com", "Mahmut Yildiz");
                msj1.To.Add(m.Email); //Buraua iletişim sayfasında gelecek mail adresi gelecktir.
                msj1.Subject = "Beni ne kadar Tanıyorsun?";
                msj1.Body = "http://8082/Home/ListofQustion";
                client.Send(msj1);
                ViewBag.Succes = "teşekkürler Mailniz başarı bir şekilde gönderildi"; //Bu kısımlarda ise kullanıcıya bilgi vermek amacı ile olur
                return View();
            }
            catch (Exception)
            {
                ViewBag.Error = "Mesaj Gönderilken hata olıuştu"; //Bu kısımlarda ise kullanıcıya bilgi vermek amacı ile olur
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
