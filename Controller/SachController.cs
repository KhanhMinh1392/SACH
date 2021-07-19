using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BenchmarkDotNet.Reports;
using SACH.Model;

namespace SACH.Controller
{
    public class SachController : ApiController
    {
        [HttpGet]
        public List<SACH> GetSachLists()
        {
            DBSachDataContext db = new DBSachDataContext();
            return db.SACHes.ToList();
        }
        [HttpGet]
        public SACH GetFood(int id)
        {
            DBSachDataContext db = new DBSachDataContext();
            return db.SACHes.FirstOrDefault(x => x.id == id);
        }
        [HttpPost]
        public bool InsertNewFood(string name, string type, int price)
        {
            try
            {
                DBSachDataContext db = new DBSachDataContext();
                SACH sach = new SACH();
                sach.AuthorName = name;
                sach.Content = type;
                sach.Price = price;
                db.SACHes.InsertOnSubmit(sach);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [HttpPut]
        public bool UpdateFood(int id, string name, string type, int price)
        {
            try
            {
                DBSachDataContext db = new DBSachDataContext();
                //lấy food tồn tại ra
                SACH sach = db.SACHes.FirstOrDefault(x => x.id == id);
                if (sach == null) return false;//không tồn tại false
                sach.AuthorName = name;
                sach.Content = type;
                sach.Price = price;
                db.SubmitChanges();//xác nhận chỉnh sửa
                return true;
            }
            catch
            {
                return false;
            }
        }
        [HttpDelete]
        public bool DeleteFood(int id)
        {
            DBSachDataContext db = new DBSachDataContext();
            //lấy food tồn tại ra
            SACH sach = db.SACHes.FirstOrDefault(x => x.id == id);
            if (sach == null) return false;
            db.SACHes.DeleteOnSubmit(sach);
            db.SubmitChanges();
            return true;
        }
    }
}
