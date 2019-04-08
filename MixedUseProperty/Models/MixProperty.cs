using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace MixedUseProperty.Models
{
    public class MixProperty
    {
        public static DataTable dtProperty { get; set; }
        public string strPropName { get; set; }
        //  public  string strNoSites { get; set; }
        public string strpropcode { get; set; }
        public string strProprep { get; set; }
        public string strPropAddress { get; set; }
        public string strEmail { get; set; }
        public string strSpace { get; set; }

        public string strSiteName { get; set; }

        public string strSiteCode { get; set; }
        public string strSiteSpace { get; set; }
        public string strSiteNoofFloors { get; set; }
        public string strSiterepName { get; set; }
        public string strSiteEmail { get; set; }
        public string strsiteRepEmail { get; set; }
        public string strSiteProID { get; set; }
        public static DataTable dtSpace { get; set; }
        public static DataTable dtSites { get; set; }
        public static DataTable dtResident { get; set; }

        //Variable Screen

        public string VarName { get; set; }
        public string VarType { get; set; }
        public string Form { get; set; }

        public DataTable GetData()
        {
            string strTemplateFilePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/Data/MixedUseProperty_data.xlsx");
            DataTable dt = ExcelHelper.GetDataTableFromExcel(strTemplateFilePath, "Property", true);
            return dt;
        }
        public DataTable GetSpaceData()
        {
            string strTemplateFilePath = HttpContext.Current.Server.MapPath("~/Content/Data/MixedUseProperty_data.xlsx");
            DataTable dt = ExcelHelper.GetDataTableFromExcel(strTemplateFilePath, "Space", true);
            return dt;
        }
        public DataTable GetSitesData()
        {
            string strTemplateFilePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/Data/MixedUseProperty_data.xlsx");
            DataTable dt = ExcelHelper.GetDataTableFromExcel(strTemplateFilePath, "Site", true);
            return dt;
        }

        public DataTable GetResidentData()
        {
            string strTemplateFilePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/Data/MixedUseProperty_data.xlsx");
            DataTable dt = ExcelHelper.GetDataTableFromExcel(strTemplateFilePath, "GetResidentData", true);
            return dt;
        }

        public string strSiteId { get; set; }
        public string strPropID { get; set; }
        

    }
    public static class ExcelHelper
    {
        public static DataTable GetDataTableFromExcel(string path, string strsheet, bool hasHeader = true)
        {
            try
            {
                bool bln = false;
                using (var pck = new OfficeOpenXml.ExcelPackage())
                {
                    using (var stream = File.OpenRead(path))
                    {
                        pck.Load(stream);
                    }
                    var worksheet = pck.Workbook.Worksheets.SingleOrDefault(x => x.Name.ToUpper().Trim() == strsheet.ToUpper());
                    if (worksheet == null)
                    {
                        bln = false;
                        if (!bln)
                        {
                            DataTable dtemp = new DataTable(); 
                            dtemp.Columns.Add(new DataColumn("Dummy_Col", Type.GetType("System.Int32"))); 
                            return dtemp;
                        }
                    }
                    using (DataTable tbl = new DataTable())
                    {
                        foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
                        {
                            if (firstRowCell.Text == "Created On")
                            {
                                var idx = worksheet.Cells["1:1"].First(c => c.Value.ToString() == "Created On").Start.Column;
                                //worksheet.Column(idx).Style.Numberformat.Format = "yyyy-mm-dd";
                                worksheet.Column(idx).Style.Numberformat.Format = "m/d/yy h:mm";

                            }
                            tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                        }
                        var startRow = hasHeader ? 2 : 1;
                        string strCellValue = string.Empty;
                        for (int rowNum = startRow; rowNum <= worksheet.Dimension.End.Row; rowNum++)
                        {
                            var wsRow = worksheet.Cells[rowNum, 1, rowNum, worksheet.Dimension.End.Column];
                            DataRow row = tbl.Rows.Add();
                            foreach (var cell in wsRow)
                            {
                                strCellValue = cell.Text;
                                if (!string.IsNullOrEmpty(strCellValue))
                                {
                                    row[cell.Start.Column - 1] = strCellValue.Trim();
                                }
                                else
                                    row[cell.Start.Column - 1] = strCellValue;
                            }
                        }
                        //tbl.Rows.RemoveAt(0);
                        return (tbl);
                    }
                }
            }
            catch (Exception ex)
            {
                DataTable dtemp = new DataTable(); 
                dtemp.Columns.Add(new DataColumn("Error", Type.GetType("System.Int32"))); 
                return dtemp;

            }
        }

        public static bool WriteToExcel(string strSheeetame, MixProperty lstData, string strpath)
        {
            try
            {
                FileInfo file = new FileInfo(strpath);
                using (ExcelPackage excelPackage = new ExcelPackage(file))
                {
                    ExcelWorkbook excelWorkBook = excelPackage.Workbook;
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[strSheeetame];
                    int iColCnt = worksheet.Dimension.End.Column;
                    int iRowCnt = worksheet.Dimension.End.Row;
                    worksheet.Cells[iRowCnt + 1, 1].Value = iRowCnt;
                    worksheet.Cells[iRowCnt + 1, 2].Value = lstData.strPropName;
                    worksheet.Cells[iRowCnt + 1, 3].Value = lstData.strpropcode;
                    worksheet.Cells[iRowCnt + 1, 4].Value = lstData.strEmail;
                    worksheet.Cells[iRowCnt + 1, 5].Value = lstData.strProprep;
                    worksheet.Cells[iRowCnt + 1, 6].Value = "Chiru";
                    worksheet.Cells[iRowCnt + 1, 7].Value = DateTime.Now;

                    excelPackage.Save();
                }
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public static bool WriteSpaceToExcel(string strSheeetame, MixProperty lstData, string strpath)
        {
            try
            {
                FileInfo file = new FileInfo(strpath);
                using (ExcelPackage excelPackage = new ExcelPackage(file))
                {
                    ExcelWorkbook excelWorkBook = excelPackage.Workbook;
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[strSheeetame];
                    int iColCnt = worksheet.Dimension.End.Column;
                    int iRowCnt = worksheet.Dimension.End.Row;
                    worksheet.Cells[iRowCnt + 1, 1].Value = iRowCnt;
                    worksheet.Cells[iRowCnt + 1, 2].Value = lstData.strSpace;
                    worksheet.Cells[iRowCnt + 1, 3].Value = "Chiru";
                    worksheet.Cells[iRowCnt + 1, 4].Value = DateTime.Now;
                    excelPackage.Save();
                }
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public static bool WriteSiteToExcel(string strSheeetame, MixProperty lstData, string strpath)
        {
            try
            {
                FileInfo file = new FileInfo(strpath);
                using (ExcelPackage excelPackage = new ExcelPackage(file))
                {
                    ExcelWorkbook excelWorkBook = excelPackage.Workbook;
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[strSheeetame];
                    int iColCnt = worksheet.Dimension.End.Column;
                    int iRowCnt = worksheet.Dimension.End.Row;
                    var query = from r in MixProperty.dtProperty.AsEnumerable()
                                where r.Field<string>("Property Name") == lstData.strSiteProID
                                select r;
                    DataTable conversions = query.CopyToDataTable();


                    worksheet.Cells[iRowCnt + 1, 1].Value = conversions.Rows[0].Field<string>(0);
                    worksheet.Cells[iRowCnt + 1, 2].Value = iRowCnt;
                    worksheet.Cells[iRowCnt + 1, 3].Value = lstData.strSiteName;
                    worksheet.Cells[iRowCnt + 1, 4].Value = lstData.strSiteCode;
                    worksheet.Cells[iRowCnt + 1, 5].Value = lstData.strSpace;
                    worksheet.Cells[iRowCnt + 1, 6].Value = lstData.strSiteNoofFloors;
                    worksheet.Cells[iRowCnt + 1, 7].Value = lstData.strSiterepName;
                    worksheet.Cells[iRowCnt + 1, 8].Value = lstData.strSiteEmail;
                    worksheet.Cells[iRowCnt + 1, 9].Value = "Chiru";
                    worksheet.Cells[iRowCnt + 1, 10].Value = DateTime.Now;

                    excelPackage.Save();
                }
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public static bool WriteToExcelVariable(string strSheeetame, MixProperty lstData, string strpath)
        {
            try
            {
                FileInfo file = new FileInfo(strpath);

                using (ExcelPackage excelPackage = new ExcelPackage(file))

                {

                    ExcelWorkbook excelWorkBook = excelPackage.Workbook;

                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[strSheeetame];

                    int iColCnt = worksheet.Dimension.End.Column;

                    int iRowCnt = worksheet.Dimension.End.Row;

                    worksheet.Cells[iRowCnt + 1, 1].Value = iRowCnt + 1;

                    worksheet.Cells[iRowCnt + 1, 2].Value = lstData.VarName;

                    worksheet.Cells[iRowCnt + 1, 3].Value = lstData.VarType;

                    worksheet.Cells[iRowCnt + 1, 4].Value = lstData.Form;





                    worksheet.Cells[iRowCnt + 1, 5].Value = "Chiru";

                    worksheet.Cells[iRowCnt + 1, 6].Value = DateTime.Now.ToString();





                    excelPackage.Save();

                }

                return true;

            }

            catch (Exception ex)

            {
                return false;

            }

        }
    }
}