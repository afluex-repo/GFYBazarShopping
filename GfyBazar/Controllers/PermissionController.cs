﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GfyBazar.Models;
using System.Data;
using GfyBazar.Filter;


namespace GfyBazar.Controllers
{
    public class PermissionController : AdminBaseController
    {

        public ActionResult SetPermission(Permisssions model)
        {
           
            DataSet ds1 = new DataSet();

            #region ddlformtype
            Common obj = new Common();
            int count = 0;
            List<SelectListItem> ddlformtype = new List<SelectListItem>();
            ds1 = obj.BindFormTypeMaster();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlformtype.Add(new SelectListItem { Text = "Select ", Value = "0" });
                    }
                    ddlformtype.Add(new SelectListItem { Text = r["FormType"].ToString(), Value = r["PK_FormTypeId"].ToString() });
                    count = count + 1;
                }
            }
            else
            {
                ddlformtype.Add(new SelectListItem { Text = "Select ", Value = "0" });
            }
            ViewBag.ddlformtype = ddlformtype;

            #endregion
            #region ddluser

            List<SelectListItem> ddluser = new List<SelectListItem>();
            Employee emp = new Employee();
            ds1 = emp.EmployeeList();
            int count1 = 0;
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddluser.Add(new SelectListItem { Text = "Select ", Value = "0" });
                    }

                    ddluser.Add(new SelectListItem { Text = r["Fullname"].ToString(), Value = r["PK_UserID"].ToString() });
                    count1 = count1 + 1;
                }
            }
            else
            {
                ddluser.Add(new SelectListItem { Text = "Select ", Value = "0" });
            }

            ViewBag.ddluser = ddluser;

            #endregion

            return View();
        }
        [HttpPost]
        [ActionName("SetPermission")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult GetPermission(Permisssions obj)
        {
            Permisssions model = new Permisssions();
            List<Permisssions> lst = new List<Permisssions>();
            DataSet ds = obj.GetFormPermission();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Permisssions ob = new Permisssions();
                    ob.FormName = dr["FormName"].ToString();
                    ob.IsSelectValue = Convert.ToBoolean(dr["FormView"].ToString());
                    if (ob.IsSelectValue == false)
                    {
                        ob.SelectedValue = "";
                    }
                    else
                    {
                        ob.SelectedValue = "checked";
                    }
                    ob.IsSaveValue = Convert.ToBoolean(dr["FormSave"].ToString());
                    ob.IsUpdateValue = Convert.ToBoolean(dr["FormUpdate"].ToString());
                    ob.IsDeleteValue = Convert.ToBoolean(dr["FormDelete"].ToString());
                    ob.Fk_FormId = dr["PK_FormId"].ToString();
                    ob.Fk_FormTypeId = dr["pk_formtypeid"].ToString();
                    ob.Fk_UserId = dr["Fk_UserId"].ToString();
                    lst.Add(ob);
                }
                model.lstpermission = lst;
            }
            DataSet ds1 = new DataSet();

            #region ddlformtype
            Common obj1 = new Common();
            List<SelectListItem> ddlformtype = new List<SelectListItem>();
            ds1 = obj1.BindFormTypeMaster();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                { ddlformtype.Add(new SelectListItem { Text = r["FormType"].ToString(), Value = r["PK_FormTypeId"].ToString() }); }
            }

            ViewBag.ddlformtype = ddlformtype;

            #endregion
            #region ddluser

            List<SelectListItem> ddluser = new List<SelectListItem>();
            Employee emp = new Employee();
            ds1 = emp.EmployeeList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                { ddluser.Add(new SelectListItem { Text = r["Fullname"].ToString(), Value = r["PK_UserID"].ToString() }); }
            }

            ViewBag.ddluser = ddluser;

            #endregion
            return View(model);
        }

        [HttpPost]
        [ActionName("SetPermission")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SavePermission(Permisssions obj)
        {
            string hdrows = Request["hdRows"].ToString();
            string chkSave = "";
            string chkupdate = "";
            string chkdelete = "";
            string chkselect = "";
            string hdfformtypeid = "";
            string hdfformid = "";
            string hdfloginid = "";
            DataTable dtpermission = new DataTable();

            dtpermission.Columns.Add("Fk_FormTypeId");
            dtpermission.Columns.Add("Fk_FormId");
            dtpermission.Columns.Add("IsSave");
            dtpermission.Columns.Add("IsUpdate");
            dtpermission.Columns.Add("IsDelete");
            dtpermission.Columns.Add("IsSelect");
            for (int i = 1; i < int.Parse(hdrows); i++)
            {
                try
                {
                    chkselect = Request["chkSelect_ " + i].ToString();
                }
                catch { chkselect = "0"; }
                hdfformtypeid = Request["hdFormtypeId_ " + i].ToString();
                hdfformid = Request["hdFormId_ " + i].ToString();
                hdfloginid = Request["hdLoginid_ " + i].ToString();

                dtpermission.Rows.Add(hdfformtypeid, hdfformid, "0", "0", "0", chkselect == "on" ? "1" : "0");

            }

            obj.UserTypeFormPermisssion = dtpermission;
            obj.CreatedBy = Session["Pk_AdminId"].ToString();
            obj.Fk_UserId = hdfloginid;
            obj.Fk_FormTypeId = hdfformtypeid;
            DataSet ds = obj.SavePermisssion();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["Permission"] = "Permission set successfully.";
                }
                else
                {
                    TempData["Permission"] = ds.Tables[0].Rows[0]["Remark"].ToString();
                }
            }

            return RedirectToAction("SetPermission");

        }

    }
}
