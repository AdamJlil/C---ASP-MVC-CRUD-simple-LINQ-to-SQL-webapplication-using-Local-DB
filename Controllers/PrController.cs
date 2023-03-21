using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class PrController : Controller
    {

        public ActionResult login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult login(Prof p)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            var res = (from Prof in db.Profs
                       where Prof.user == p.user && Prof.pass == p.pass
                       select Prof.IdProf).SingleOrDefault();

            if (res == 0)
            {

                ViewBag.err = "Mot de passe ou Username Incorrecte!";
                return View();
            }

            IdProf.Id = res;
            return RedirectToAction("loged");




        }







        public ActionResult loged()
        {
            if (IdProf.Id == null)
            {
                RedirectToAction("login");
            }
            else
            {
                DataClasses1DataContext db = new DataClasses1DataContext();
                int? idProf = IdProf.Id;

                String res = (from Prof in db.Profs
                              where Prof.IdProf == idProf
                              select Prof.user).SingleOrDefault();
                ViewBag.user = res;

                List<St> res_st = (from st in db.Sts
                                   select st).ToList<St>();
                return View("loged", res_st);


            }



            return View();
        }


        public ActionResult plagehoraire()
        {

            List<plg_horaire> ls = new List<plg_horaire>();

            DataClasses1DataContext db = new DataClasses1DataContext();

            for(int i = 1; i <= 8; i++)
            {
             
                plg_horaire res = (from ph in db.plg_horaires
                                         where ph.IdPlg == i
                                         select ph).SingleOrDefault();

                ls.Add(res);
                                     
            }
            

            return View("plagehoraire", ls);

        }

        
        [HttpPost]
        public ActionResult plagehoraire(String day, String session)
        {
            List<String> dayNum = new List<String>
            {
                "monday",
                "tuesday",
                "wednesday",
                "thursday",
                "friday"
            };

            string day_ = day;
            string session_ = session.Substring(session.Length-1);

            DataClasses1DataContext db = new DataClasses1DataContext();

            int pos = dayNum.IndexOf(day_.ToLower());
             
             if (IdProf.Id != null && pos>=0)
            {
                String prof = (from a in db.Profs
                              where a.IdProf == IdProf.Id
                              select a.user).SingleOrDefault();

                int idpos = int.Parse(session_);

                plg_horaire res = (from ph in db.plg_horaires
                                   where ph.IdPlg == idpos
                                   select ph).SingleOrDefault<plg_horaire>();
                
                if (pos == 0)
                {
                    if (!res.L.Contains("r"))
                    {
                        res.L = "r" + prof;

                    }
                    else
                    {
                        ViewBag.msg = "This session is already selected!";
                    }
                    
                }
                else if (pos == 1)
                {
                    if (!res.M.Contains("r"))
                    {
                        res.M = "r" + prof;

                    }
                    else
                    {
                        ViewBag.msg = "This session is already selected!";
                    }
                }
                else if (pos == 2)
                {
                    if (!res.Mer.Contains("r"))
                    {
                        res.Mer = "r" + prof;

                    }
                    else
                    {
                        ViewBag.msg = "This session is already selected!";
                    }
                }
                else if (pos == 3)
                {
                    if (!res.J.Contains("r"))
                    {
                        res.J = "r" + prof;

                    }
                    else
                    {
                        ViewBag.msg = "This session is already selected!";
                    }
                }
                else if (pos == 4)
                {
                    if (!res.V.Contains("r"))
                    {
                        res.V = "r" + prof;

                    }
                    else
                    {
                        ViewBag.msg = "This session is already selected!";
                    }
                }

                db.SubmitChanges();

            }
            



            return RedirectToAction("plagehoraire");
        }



        public ActionResult Delplagehoraire()
        {
            List<plg_horaire> ls = new List<plg_horaire>();

            DataClasses1DataContext db = new DataClasses1DataContext();

            for (int i = 1; i <= 8; i++)
            {

                plg_horaire res = (from ph in db.plg_horaires
                                   where ph.IdPlg == i
                                   select ph).SingleOrDefault();

                ls.Add(res);

            }


            return View("Delplagehoraire", ls);


        }





        [HttpPost]
        public ActionResult Delplagehoraire(String day, String session)
        {

            List<String> dayNum = new List<String>
            {
                "monday",
                "tuesday",
                "wednesday",
                "thursday",
                "friday"
            };

            string day_ = day;
            string session_ = session.Substring(session.Length - 1);

            DataClasses1DataContext db = new DataClasses1DataContext();

            int pos = dayNum.IndexOf(day_.ToLower());


            if (IdProf.Id != null && pos >= 0)
            {
                String prof = (from a in db.Profs
                               where a.IdProf == IdProf.Id
                               select a.user).SingleOrDefault();

                int idpos = int.Parse(session_);

                plg_horaire res = (from ph in db.plg_horaires
                                   where ph.IdPlg == idpos
                                   select ph).SingleOrDefault<plg_horaire>();

                if (pos == 0)
                {
                    if (!res.L.Contains("n"))
                    {
                        res.L = "n";

                    }
                    else
                    {
                        ViewBag.msg = "This session is already Non-Reserved!";
                    }

                }
                else if (pos == 1)
                {
                    if (!res.M.Contains("r"))
                    {
                        res.M = "n";

                    }
                    else
                    {
                        ViewBag.msg = "This session is already Non-Reserved!";
                    }
                }
                else if (pos == 2)
                {
                    if (!res.Mer.Contains("r"))
                    {
                        res.Mer = "n";

                    }
                    else
                    {
                        ViewBag.msg = "This session is already Non-Reserved!";
                    }
                }
                else if (pos == 3)
                {
                    if (!res.J.Contains("r"))
                    {
                        res.J = "n";

                    }
                    else
                    {
                        ViewBag.msg = "This session is already Non-Reserved!";
                    }
                }
                else if (pos == 4)
                {
                    if (!res.V.Contains("r"))
                    {
                        res.V = "n";

                    }
                    else
                    {
                        ViewBag.msg = "This session is already Non-Reserved!";
                    }
                }

                db.SubmitChanges();

            }




            return RedirectToAction("Delplagehoraire");


        }












        //MANAGEMENT STUDENTS LIST

        public ActionResult listStudents()
        {

            if (IdProf.Id == 0)
            {
                RedirectToAction("login");
            }
            else
            {
                DataClasses1DataContext db = new DataClasses1DataContext();
                int? idProf = IdProf.Id;

                String res = (from Prof in db.Profs
                              where Prof.IdProf == idProf
                              select Prof.user).SingleOrDefault();
                ViewBag.user = res;

                List<St> res_st = (from st in db.Sts
                                   select st).ToList<St>();
                return View("listStudents", res_st);


            }



            return View();

        }


        [HttpPost]
        public ActionResult listStudents(String Idst)
        {

            String action = Idst.Substring(0, 3);
            int idSt = Convert.ToInt32(Idst.Substring(3));

            DataClasses1DataContext db = new DataClasses1DataContext();

            var res = (from st in db.Sts
                       where st.IdSt == idSt
                       select st).SingleOrDefault();

            if (action == "del")
            {

                res.auto = "no";
            }
            else if (action == "sus")
            {
                
                res.auto = "sus";
            }
            else if (action == "add")
            {
                res.auto = "access";
            }

            db.SubmitChanges();


            return RedirectToAction("listStudents");

        }





    }
}