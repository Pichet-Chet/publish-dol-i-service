using System;
using DOL.API.Models.Constants;
using DOL.API.Models.Customs.Response;
using DOL.API.Models.PaperGeneration;
using DOL.API.Models.Response;

namespace DOL.API.Services.GenerateDocument
{
    public class RepairMonthPaper
    {
        public RepairMonthPaper()
        {
        }

        public async Task<Response> GetnerateDocument(ExportJobRepairMonthReponse param)
        {
            Response result = new Response();



            try
            {
                if (param != null)
                {

                    if (param.page == 1)
                    {
                        int fontSize = 20;

                        int positionXhour = 530;

                        PaperGeneration paper = new PaperGeneration("repair-month-1.jpg", fontSize);

                        paper.addText(param.MonthName, 315, 150);
                        paper.addText(param.Year, 430, 150);

                        paper.addText(Convert.ToString(param.Fiber1), positionXhour, 241);
                        paper.addText(Convert.ToString(param.Fiber2), positionXhour, 266);
                        paper.addText(Convert.ToString(param.Fiber3), positionXhour, 292);
                        paper.addText(Convert.ToString(param.Fiber4), positionXhour, 317);
                        paper.addText(Convert.ToString(param.Fiber5), positionXhour, 344);
                        paper.addText(Convert.ToString(param.Fiber6), positionXhour, 369);
                        paper.addText(Convert.ToString(param.Fiber7), positionXhour, 396);
                        paper.addText(Convert.ToString(param.Fiber8), positionXhour, 422);
                        paper.addText(Convert.ToString(param.Fiber9), positionXhour, 448);
                        paper.addText(Convert.ToString(param.Fiber10), positionXhour, 474);
                        paper.addText(Convert.ToString(param.Fiber11), positionXhour, 500);
                        paper.addText(Convert.ToString(param.Fiber12), positionXhour, 526);
                        paper.addText(Convert.ToString(param.Fiber13), positionXhour, 552);
                        paper.addText(Convert.ToString(param.Fiber14), positionXhour, 578);
                        paper.addText(Convert.ToString(param.Fiber15), positionXhour, 606);
                        paper.addText(Convert.ToString(param.Fiber16), positionXhour, 630);

                        paper.addText(Convert.ToString(param.Network1), positionXhour, 700);
                        paper.addText(Convert.ToString(param.Network2), positionXhour, 726);
                        paper.addText(Convert.ToString(param.Network3), positionXhour, 753);
                        paper.addText(Convert.ToString(param.Network4), positionXhour, 779);
                        paper.addText(Convert.ToString(param.Network5), positionXhour, 804);
                        paper.addText(Convert.ToString(param.Network6), positionXhour, 829);
                        paper.addText(Convert.ToString(param.Network7), positionXhour, 857);
                        paper.addText(Convert.ToString(param.Network8), positionXhour, 882);

                        byte[] streamResult = paper.getPaper();

                        result.data = streamResult;
                        result.httpCode = Constants.httpCode200;
                        result.message = Constants.httpCode200Message;
                    }
                    else if (param.page == 2)
                    {
                        int fontSize = 20;

                        int positionXhour = 530;

                        int defaultStart = 120;

                        float cal = 26.20f;

                        PaperGeneration paper = new PaperGeneration("repair-month-2.jpg", fontSize);

                        paper.addText(Convert.ToString(param.Network9), positionXhour, defaultStart + (cal * 0));
                        paper.addText(Convert.ToString(param.Network10), positionXhour, defaultStart + (cal * 1));
                        paper.addText(Convert.ToString(param.Network11), positionXhour, defaultStart + (cal * 2));
                        paper.addText(Convert.ToString(param.Network12), positionXhour, defaultStart + (cal * 3));
                        paper.addText(Convert.ToString(param.Network13), positionXhour, defaultStart + (cal * 4));
                        paper.addText(Convert.ToString(param.Network14), positionXhour, defaultStart + (cal * 5));
                        paper.addText(Convert.ToString(param.Network15), positionXhour, defaultStart + (cal * 6));
                        paper.addText(Convert.ToString(param.Network16), positionXhour, defaultStart + (cal * 7));
                        paper.addText(Convert.ToString(param.Network17), positionXhour, defaultStart + (cal * 8));
                        paper.addText(Convert.ToString(param.Network18), positionXhour, defaultStart + (cal * 9));
                        paper.addText(Convert.ToString(param.Network19), positionXhour, defaultStart + (cal * 10));
                        paper.addText(Convert.ToString(param.Network20), positionXhour, defaultStart + (cal * 11));
                        paper.addText(Convert.ToString(param.Network21), positionXhour, defaultStart + (cal * 12));
                        paper.addText(Convert.ToString(param.Network22), positionXhour, defaultStart + (cal * 13));
                        paper.addText(Convert.ToString(param.Network23), positionXhour, defaultStart + (cal * 14));
                        paper.addText(Convert.ToString(param.Network24), positionXhour, defaultStart + (cal * 15));
                        paper.addText(Convert.ToString(param.Network25), positionXhour, defaultStart + (cal * 16));
                        paper.addText(Convert.ToString(param.Network26), positionXhour, defaultStart + (cal * 17));
                        paper.addText(Convert.ToString(param.Network27), positionXhour, defaultStart + (cal * 18));
                        paper.addText(Convert.ToString(param.Network28), positionXhour, defaultStart + (cal * 19));
                        paper.addText(Convert.ToString(param.Network29), positionXhour, defaultStart + (cal * 20));
                        paper.addText(Convert.ToString(param.Network30), positionXhour, defaultStart + (cal * 21));
                        paper.addText(Convert.ToString(param.Network31), positionXhour, defaultStart + (cal * 22));
                        paper.addText(Convert.ToString(param.Network32), positionXhour, defaultStart + (cal * 23));
                        paper.addText(Convert.ToString(param.Network33), positionXhour, defaultStart + (cal * 24));
                        paper.addText(Convert.ToString(param.Network34), positionXhour, defaultStart + (cal * 25));

                        paper.addText(Convert.ToString(param.Customer1), positionXhour, 843);
                        paper.addText(Convert.ToString(param.Customer2), positionXhour, 868);
                        paper.addText(Convert.ToString(param.Customer3), positionXhour, 894);


                        byte[] streamResult = paper.getPaper();

                        result.data = streamResult;
                        result.httpCode = Constants.httpCode200;
                        result.message = Constants.httpCode200Message;
                    }
                    else if (param.page == 3)
                    {
                        int fontSize = 20;

                        int positionXhour = 530;

                        int defaultStart = 95;

                        float cal = 26.20f;

                        PaperGeneration paper = new PaperGeneration("repair-month-3.jpg", fontSize);


                        paper.addText(Convert.ToString(param.Customer4), positionXhour, defaultStart + (cal * 1));
                        paper.addText(Convert.ToString(param.Customer5), positionXhour, defaultStart + (cal * 2));
                        paper.addText(Convert.ToString(param.Customer6), positionXhour, defaultStart + (cal * 3));
                        paper.addText(Convert.ToString(param.Customer7), positionXhour, defaultStart + (cal * 4));
                        paper.addText(Convert.ToString(param.Customer8), positionXhour, defaultStart + (cal * 5));
                        paper.addText(Convert.ToString(param.Customer9), positionXhour, defaultStart + (cal * 6));
                        paper.addText(Convert.ToString(param.Customer10), positionXhour, defaultStart + (cal * 7));
                        paper.addText(Convert.ToString(param.Customer11), positionXhour, defaultStart + (cal * 8));
                        paper.addText(Convert.ToString(param.Customer12), positionXhour, defaultStart + (cal * 9));
                        paper.addText(Convert.ToString(param.Customer13), positionXhour, defaultStart + (cal * 10));
                        paper.addText(Convert.ToString(param.Customer14), positionXhour, defaultStart + (cal * 11));
                        paper.addText(Convert.ToString(param.Customer15), positionXhour, defaultStart + (cal * 12));
                        paper.addText(Convert.ToString(param.Customer16), positionXhour, defaultStart + (cal * 13));
                        paper.addText(Convert.ToString(param.Customer17), positionXhour, defaultStart + (cal * 14));
                        paper.addText(Convert.ToString(param.Customer18), positionXhour, defaultStart + (cal * 15));
                        paper.addText(Convert.ToString(param.Customer19), positionXhour, defaultStart + (cal * 16));
                        paper.addText(Convert.ToString(param.Customer20), positionXhour, defaultStart + (cal * 17));
                        paper.addText(Convert.ToString(param.Customer21), positionXhour, defaultStart + (cal * 18));
                        paper.addText(Convert.ToString(param.Customer22), positionXhour, defaultStart + (cal * 19));
                        paper.addText(Convert.ToString(param.Customer23), positionXhour, defaultStart + (cal * 20));
                        paper.addText(Convert.ToString(param.Customer24), positionXhour, defaultStart + (cal * 21));

                        paper.addText(Convert.ToString(param.Other1), positionXhour, 713);
                        paper.addText(Convert.ToString(param.Other2), positionXhour, 740);
                        paper.addText(Convert.ToString(param.Other3), positionXhour, 766);



                        byte[] streamResult = paper.getPaper();

                        result.data = streamResult;
                        result.httpCode = Constants.httpCode200;
                        result.message = Constants.httpCode200Message;
                    }

                }
                else
                {
                    result.status = false;
                    result.message = "ไม่สามารถพิมพ์เอกสารได้ เนื่องจากเป็นหน่วยงานส่วนกลาง";
                    result.httpCode = Constants.httpCode400;
                }

            }
            catch (Exception ex)
            {
                result.exception = ex.Message;
            }

            return result;
        }
    }
}

