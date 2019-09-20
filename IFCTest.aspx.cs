using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using Xbim.CobieExpress;
using Xbim.Common;
using Xbim.Ifc;
using Xbim.Ifc2x3.Interfaces;
using Xbim.Ifc2x3.Kernel;
using Xbim.Ifc2x3.MeasureResource;
using Xbim.Ifc2x3.PropertyResource;
using Xbim.IO;
using Xbim.IO.CobieExpress;
using Xbim.IO.Memory;
using Xbim.ModelGeometry.Scene;
using Xbim.CobieExpress.Exchanger;

namespace WebApplication2
{
    public partial class IFCTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            


        }

        protected void btnConversionfromIFC_Click(object sender, EventArgs e)
        {
            string ConversionPath = Server.MapPath("~/ConversionFiles/") + Path.GetFileName(FileUploadIFC.PostedFile.FileName);
            FileUploadIFC.SaveAs(ConversionPath);

            ConvertIfcToCoBieExpress();

            ConvertIFCtoWEXBIM(ConversionPath);
           
        }

        protected void ConvertIFCtoWEXBIM(string filePath)
        {
            var IfcTestFile = filePath;
            using (var model = IfcStore.Open(IfcTestFile))
            {
                var context = new Xbim3DModelContext(model);
                context.CreateContext();

                var wexBimFilename = Path.ChangeExtension(IfcTestFile, "wexbim");
                using (var wexBiMfile = File.Create(wexBimFilename))
                {
                    using (var wexBimBinaryWriter = new BinaryWriter(wexBiMfile))
                    {
                        model.SaveAsWexBim(wexBimBinaryWriter);
                        wexBimBinaryWriter.Close();
                    }
                    wexBiMfile.Close();
                }
            }
        }


        public void ConvertIfcToCoBieExpress()
        {
            string input = Server.MapPath("~/ConversionFiles/") + Path.GetFileName(FileUploadIFC.PostedFile.FileName);
            //const string input = @"SampleHouse4.ifc";
            var inputInfo = new FileInfo(input);
            var ifc = MemoryModel.OpenReadStep21(input);
            var inputCount = ifc.Instances.Count;

            var w = new Stopwatch();
            var cobie = new CobieModel();
            using (var txn = cobie.BeginTransaction("Sample house conversion"))
            {
                var exchanger = new IfcToCoBieExpressExchanger(ifc, cobie);
                w.Start();
                exchanger.Convert();
                w.Stop();
                txn.Commit();
            }
            var output = Path.ChangeExtension(input, ".cobie");
            cobie.SaveAsStep21(output);

            var outputInfo = new FileInfo(output);
            Console.WriteLine("Time to convert {0:N}MB file ({2} entities): {1}ms", inputInfo.Length / 1e6f, w.ElapsedMilliseconds, inputCount);
            Console.WriteLine("Resulting size: {0:N}MB ({1} entities)", outputInfo.Length / 1e6f, cobie.Instances.Count);

            using (var txn = cobie.BeginTransaction("Renaming"))
            {
                MakeUniqueNames<CobieFacility>(cobie);
                MakeUniqueNames<CobieFloor>(cobie);
                MakeUniqueNames<CobieSpace>(cobie);
                MakeUniqueNames<CobieZone>(cobie);
                MakeUniqueNames<CobieComponent>(cobie);
                MakeUniqueNames<CobieSystem>(cobie);
                MakeUniqueNames<CobieType>(cobie);
                txn.Commit();
            }

            //save as XLSX
            output = Path.ChangeExtension(input, ".xlsx");
            cobie.ExportToTable(output, out string report);
        }

        private static void MakeUniqueNames<T>(IModel model) where T : CobieAsset
        {
            var groups = model.Instances.OfType<T>().GroupBy(a => a.Name);
            foreach (var @group in groups)
            {
                if (group.Count() == 1)
                {
                    var item = group.First();
                    if (string.IsNullOrEmpty(item.Name))
                        item.Name = item.ExternalObject.Name;
                    continue;
                }

                var counter = 1;
                foreach (var item in group)
                {
                    if (string.IsNullOrEmpty(item.Name))
                        item.Name = item.ExternalObject.Name;
                    item.Name = string.Format("{0} ({1})", item.Name, counter++);
                }
            }
        }
    }
}