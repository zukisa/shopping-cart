using Api.Models;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using Novacode;
using Spire.Doc;
using System;
using System.IO;
using System.Linq;

namespace Api.Utils
{
    /// <summary>
    /// Generate documents
    /// </summary>
    public static class DocumentHelper
    {
        /// <summary>
        /// Convert Word document to PDF
        /// </summary>
        /// <param name="docxFile"></param>
        /// <param name="pdfFile"></param>
        /// <returns></returns>
        public static Tuple<string,  byte[]> ConvertToPDF(string docxFile, string pdfFile)
        {
            Document document = new Document();
            document.LoadFromFile(docxFile);
            document.SaveToFile(pdfFile, FileFormat.PDF);
            Tuple<string, byte[]> file = new Tuple<string, byte[]>(Path.GetFileName(pdfFile), File.ReadAllBytes(pdfFile));
            if (File.Exists(docxFile))
                File.Delete(docxFile);
            if (File.Exists(pdfFile))
                File.Delete(pdfFile);
            return file;
        }
        /// <summary>
        /// Generate Order document
        /// </summary>
        /// <param name="order"></param>
        /// <param name="request"></param>
        /// <param name="user"></param>
        /// <param name="templatePath"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static Tuple<string, byte[]> GenerateOrder(Order order, CreateOrderRequest request, User user, string templatePath, IConfiguration configuration)
        {
            string templateFile = $"{templatePath}{configuration.GetValue<string>("AppSettings:FileName")}";
            string fileName = string.Empty;
            using var document = DocX.Load(templateFile);
            document.ReplaceText("{OrderNum}", order.Id);
            document.ReplaceText("{InvDate}", DateTime.Now.ToString("yyyy-MM-dd"));
            document.ReplaceText("{Email}", user.Email);
            var items = request.ProductOrders.Select(_ => new
            {
                Description = _.ProductName,
                _.Quantity,
                Price = _.Price.AddCurrency(),
                Amount = (_.Price * _.Quantity).AddCurrency(),
                SubTotal = (_.Price * _.Quantity)
            }).ToList();
            document.ReplaceText("{Description}", string.Join(Environment.NewLine, items.Select(_ => _.Description)));
            document.ReplaceText("{Quantity}", string.Join(Environment.NewLine, items.Select(_ => _.Quantity)));
            document.ReplaceText("{Price}", string.Join(Environment.NewLine, items.Select(_ => _.Price)));
            document.ReplaceText("{Amount}", string.Join(Environment.NewLine, items.Select(_ => _.Amount)));

            decimal subTotal = items.Sum(_ => _.SubTotal);
            decimal vat = subTotal * (decimal.Parse(configuration.GetValue<string>("AppSettings:VAT")) / 100M);

            document.ReplaceText("{SubTotal}", (subTotal - vat).AddCurrency());
            document.ReplaceText("{Total}", subTotal.AddCurrency());
            document.ReplaceText("{Vat}", vat.AddCurrency());

            fileName = Path.Combine(templatePath, $"{order.Id}.docx");
            document.SaveAs(fileName);

            var fileExtension = Path.GetExtension(fileName);

            var pdfFileName = $@"{Path.GetDirectoryName(fileName)}\{Path.GetFileName(fileName).Replace(fileExtension, ".pdf")}";

            return ConvertToPDF(fileName, pdfFileName);
            

        }
        /// <summary>
        /// Convert decimal to string and add currency format
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="currencyFormat"></param>
        /// <returns></returns>
        public static string AddCurrency(this decimal amount, string currencyFormat = "R0.00") => amount.ToString(currencyFormat);
    }
}
