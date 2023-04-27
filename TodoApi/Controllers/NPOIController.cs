using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Threading.Tasks;
using TodoApi.Models.ApiResult;
using TodoApi.Models;
using System;

namespace TodoApi.Controllers
{
    /// <summary>
    /// NPOI的使用
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class NPOIController : ControllerBase
    {
        /// <summary>
        /// 上传Excel文件，读取并保存其中的数据
        /// </summary>
        /// <param name="file">上传的Excel文件</param>
        /// <returns>包含Excel文件数据的ApiResult对象</returns>
        [HttpPost, Route("UploadExcel")]
        public async Task<ActionResult<ApiResult>> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("没有选择文件");
            }

            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Content", "Upload", fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var data = new List<string>();

            IWorkbook workbook = null;

            string extension = Path.GetExtension(filePath);
            FileStream stream = System.IO.File.OpenRead(filePath);
            if (extension.Equals(".xls"))
            {
                //把xls文件中的数据写入wk中
                workbook = new HSSFWorkbook(stream);
            }
            else
            {
                //把xlsx文件中的数据写入wk中
                workbook = new XSSFWorkbook(stream);
            }

            var sheet = workbook.GetSheetAt(0);

            for (int i = 0; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                if (row == null) continue;

                var rowData = "";
                for (int j = 0; j < row.LastCellNum; j++)
                {
                    if (row.GetCell(j) != null)
                    {
                        rowData += row.GetCell(j).ToString() + ", ";
                    }
                }
                if (!string.IsNullOrEmpty(rowData))
                {
                    data.Add(rowData);
                }
            }

            // 删除上传的Excel文件
            System.IO.File.Delete(filePath);

            return ApiResultHelper.Success(data);
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("ExportExcel")]
        public IActionResult ExportExcel()
        {
            // 创建一个空的Excel文件
            var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet("Sheet1");

            // 创建表头
            var headerRow = sheet.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("姓名");
            headerRow.CreateCell(1).SetCellValue("年龄");

            // 填充数据
            List<Student> students = new List<Student>();
            for (int i = 0; i < 5; i++)
            {
                Student student = new Student();
                student.Name = "测试姓名" + (i + 1) + "";
                student.Age = 18;
                students.Add(student);
            }

            IRow row;
            ICell cell;
            for (int i = 0; i < students.Count; i++)
            {
                row = sheet.CreateRow(i + 1);//创建第i行
                for (int j = 0; j < 2; j++)//默认两列
                {
                    cell = row.CreateCell(j);//创建第j列
                    if (j == 0)
                    {
                        SetCellValue(cell, students[i].Name);
                    }
                    else
                    {
                        SetCellValue(cell, students[i].Age);
                    }
                }
            }

            // 将Excel文件转换为字节数组
            byte[] fileContents;
            using (var stream = new MemoryStream())
            {
                workbook.Write(stream);
                fileContents = stream.ToArray();
            }

            // 返回Excel文件
            return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "output.xlsx");
        }

        /// <summary>
        /// 根据数据类型设置不同类型的cell
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="obj"></param>
        public static void SetCellValue(ICell cell, object obj)
        {
            if (obj.GetType() == typeof(int))
            {
                cell.SetCellValue((int)obj);
            }
            else if (obj.GetType() == typeof(double))
            {
                cell.SetCellValue((double)obj);
            }
            else if (obj.GetType() == typeof(IRichTextString))
            {
                cell.SetCellValue((IRichTextString)obj);
            }
            else if (obj.GetType() == typeof(string))
            {
                cell.SetCellValue(obj.ToString());
            }
            else if (obj.GetType() == typeof(DateTime))
            {
                cell.SetCellValue((DateTime)obj);
            }
            else if (obj.GetType() == typeof(bool))
            {
                cell.SetCellValue((bool)obj);
            }
            else
            {
                cell.SetCellValue(obj.ToString());
            }
        }
    }

}
