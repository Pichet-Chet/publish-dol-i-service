using System;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using ClosedXML.Excel;
using DOL.API.Models.Customs.Response;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using MetadataExtractor;
using DOL.API.Models.Customs;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace DOL.API.Services.Helper
{
    public class ServiceHelper
    {
        public ServiceHelper()
        {
        }

        public static string GenarateDocumentNo(string type, string network)
        {
            string result = "";

            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");

            DateTime dt = DateTime.Now;

            ThaiBuddhistCalendar thaiCalendar = new ThaiBuddhistCalendar();

            result = Convert.ToString(thaiCalendar.GetYear(dt));

            return result;
        }

        public static DateTime gmtPlus7(DateTime param)
        {
            TimeZoneInfo gmtPlus7 = TimeZoneInfo.FindSystemTimeZoneById("Asia/Bangkok");

            return TimeZoneInfo.ConvertTimeFromUtc(param, gmtPlus7);
        }

        public static async Task<long> GetFileSizeAsync(string url)
        {
            long output = 0;

            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Head, url))
                    {
                        using (var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                        {
                            response.EnsureSuccessStatusCode(); // Throw if HTTP error status

                            // Check if Content-Length header exists
                            if (response.Content.Headers.ContentLength.HasValue)
                            {
                                output = response.Content.Headers.ContentLength.Value;
                            }
                            else
                            {
                                throw new InvalidOperationException("Content-Length header not found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return output;


        }

        public static double ConvertBytesToMegabytes(long bytes)
        {
            double output = 0;

            try
            {
                output=  Math.Round((double)bytes / 1048576, 2);
            }
            catch (Exception ex)
            {

            }

            return output;
            
        }

        public static byte[] ExportExcel(List<ExportJobRepairResponse> param)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sheet1");

                // Adding Headers
                worksheet.Cell(1, 1).Value = "ลำดับที่";
                worksheet.Cell(1, 2).Value = "เลขที่ใบแจ้งเหตุ";
                worksheet.Cell(1, 3).Value = "จังหวัด";
                worksheet.Cell(1, 4).Value = "ชื่อสำนักงาน";
                worksheet.Cell(1, 5).Value = "หมายเลขวงจร";
                worksheet.Cell(1, 6).Value = "ความเร็ว";
                worksheet.Cell(1, 7).Value = "ประเภทวงจร";
                worksheet.Cell(1, 8).Value = "สาเหตุของปัญหา";
                worksheet.Cell(1, 9).Value = "แจ้งเหตุขัดข้อง";
                worksheet.Cell(1, 10).Value = "รับแจ้ง";
                worksheet.Cell(1, 11).Value = "เริ่มตรวจสอบ";
                worksheet.Cell(1, 12).Value = "วันที่แก้ไขเสร็จ";
                worksheet.Cell(1, 13).Value = "เวลาตอบสนอง";
                worksheet.Cell(1, 14).Value = "เริ่มดำเนินการ";
                worksheet.Cell(1, 15).Value = "เหตุได้รับการแก้ไข";
                worksheet.Cell(1, 16).Value = "ชื่อผู้รับแจ้งเหตุ";
                worksheet.Cell(1, 17).Value = "หมายเหตุ";
                worksheet.Cell(1, 18).Value = "ผู้ให้บริการ";

                // Adding Data
                for (int i = 0; i < param.Count; i++)
                {
                    var model = param[i];
                    worksheet.Cell(i + 2, 1).Value = model.Seq;
                    worksheet.Cell(i + 2, 2).Value = model.DocumentNo;
                    worksheet.Cell(i + 2, 3).Value = model.ProvinceName;
                    worksheet.Cell(i + 2, 4).Value = model.SiteNetworkName;
                    worksheet.Cell(i + 2, 5).Value = model.CircuitNo;
                    worksheet.Cell(i + 2, 6).Value = model.Speed;
                    worksheet.Cell(i + 2, 7).Value = model.CircuitType;
                    worksheet.Cell(i + 2, 8).Value = model.IssueCase;
                    worksheet.Cell(i + 2, 9).Value = model.JobRequestDate;
                    worksheet.Cell(i + 2, 10).Value = model.JobAcceptDate;
                    worksheet.Cell(i + 2, 11).Value = model.JobOnProcessDate;
                    worksheet.Cell(i + 2, 12).Value = model.JobFinishDate;
                    worksheet.Cell(i + 2, 13).Value = model.ResponseTime;
                    worksheet.Cell(i + 2, 14).Value = model.OnProcessTime;
                    worksheet.Cell(i + 2, 15).Value = model.AllTimeProcess;
                    worksheet.Cell(i + 2, 16).Value = model.StaffName;
                    worksheet.Cell(i + 2, 17).Value = model.Remark;
                    worksheet.Cell(i + 2, 18).Value = model.ProviderName;
                }

                // Save to memory stream
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    // Return file as Excel
                    return content;
                }
            }
        }

        public static byte[] ConvertImageToPdf(byte[] imageData)
        {
            // Create a MemoryStream to hold the PDF content
            using (MemoryStream ms = new MemoryStream())
            {
                // Create a new Document
                using (Document document = new Document(PageSize.A4))
                {
                    // Create a PdfWriter to write to the MemoryStream
                    PdfWriter writer = PdfWriter.GetInstance(document, ms);

                    // Open the document
                    document.Open();

                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageData);

                    // Scale the image to fit within the A4 page dimensions while maintaining aspect ratio
                    image.ScaleToFit(PageSize.A4.Width - document.LeftMargin - document.RightMargin,
                                     PageSize.A4.Height - document.TopMargin - document.BottomMargin);

                    // Center the image on the page
                    image.Alignment = Element.ALIGN_CENTER;

                    // Add the image to the document
                    document.Add(image);

                    document.Close();
                }

                // Return the PDF content as a byte array
                return ms.ToArray();
            }
        }

        public static async Task GetImageInfoAsync(string imageUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                // Set the timeout to 30 seconds
                client.Timeout = TimeSpan.FromSeconds(30);

                try
                {
                    // Download the image as a stream
                    using (Stream httpStream = await client.GetStreamAsync(imageUrl))
                    {
                        // Create a MemoryStream from the downloaded stream
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            await httpStream.CopyToAsync(memoryStream);
                            memoryStream.Seek(0, SeekOrigin.Begin);

                            using (Bitmap bitmap = new Bitmap(memoryStream))
                            {
                                // Get dimensions
                                int width = bitmap.Width;
                                int height = bitmap.Height;

                                // Get resolution
                                float horizontalResolution = bitmap.HorizontalResolution;
                                float verticalResolution = bitmap.VerticalResolution;

                                // Get orientation (if available)
                                string orientation = "Unknown";
                                if (Array.IndexOf(bitmap.PropertyIdList, 274) > -1)
                                {
                                    int orientationValue = (int)bitmap.GetPropertyItem(274).Value[0];
                                    switch (orientationValue)
                                    {
                                        case 1:
                                            orientation = "Normal";
                                            break;
                                        case 8:
                                            orientation = "Rotated 270° CW";
                                            break;
                                        case 3:
                                            orientation = "Rotated 180°";
                                            break;
                                        case 6:
                                            orientation = "Rotated 90° CW";
                                            break;
                                        default:
                                            orientation = "Unknown";
                                            break;
                                    }
                                }

                                // Print image info
                                Console.WriteLine($"Width: {width} pixels");
                                Console.WriteLine($"Height: {height} pixels");
                                Console.WriteLine($"Horizontal Resolution: {horizontalResolution} dpi");
                                Console.WriteLine($"Vertical Resolution: {verticalResolution} dpi");
                                Console.WriteLine($"Orientation: {orientation}");
                            }
                        }
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request error: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }
        }

        public static ImageInfoModel ExtractMetadataFromUrl(string imageUrl)
        {
            ImageInfoModel imageInfo = new ImageInfoModel();

            try
            {
                // Download the image from the URL
                byte[] imageData;
                using (WebClient client = new WebClient())
                {
                    imageData = client.DownloadData(imageUrl);
                }

                // Extract metadata from the downloaded image
                var directories = ImageMetadataReader.ReadMetadata(new MemoryStream(imageData));

                // Iterate through metadata directories
                foreach (var directory in directories)
                {
                    foreach (var tag in directory.Tags)
                    {
                        // Check if the tag represents image width or height
                        if (tag.Name == "Image Width")
                        {
                            imageInfo.Width = Convert.ToInt32(tag.Description.Replace(" pixels",""));
                        }

                        if (tag.Name == "Image Height")
                        {
                            imageInfo.Height = Convert.ToInt32(tag.Description.Replace(" pixels", ""));
                        }

                        

                        if (tag.Name == "Orientation")
                        {
                            if (imageInfo.Width != imageInfo.Height)
                            {
                                // Adjust width and height based on orientation
                                if (tag.Description.ToLower() == "Top, Left side (Horizontal / normal)".ToLower())
                                {
                                    // No rotation needed
                                }
                                else if (tag.Description.ToLower() == "Top, Right side (Mirror horizontal)".ToLower())
                                {
                                    // Rotate 90 degrees CW
                                    int temp = imageInfo.Width;
                                    imageInfo.Width = imageInfo.Height;
                                    imageInfo.Height = temp;
                                }
                                else if (tag.Description.ToLower() == "Bottom, Right side (Rotate 180)".ToLower())
                                {
                                    // Rotate 180 degrees
                                }
                                else if (tag.Description.ToLower() == "Bottom, Left side (Mirror vertical)".ToLower())
                                {
                                    // Rotate 90 degrees CCW
                                    int temp = imageInfo.Width;
                                    imageInfo.Width = imageInfo.Height;
                                    imageInfo.Height = temp;
                                }
                                else if (tag.Description.ToLower() == "Right side, top (Rotate 90 CW)".ToLower())
                                {
                                    // Rotate 90 degrees CCW
                                    int temp = imageInfo.Width;
                                    imageInfo.Width = imageInfo.Height;
                                    imageInfo.Height = temp;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return imageInfo;
        }
    }
}

