using System;
using DOL.API.Models.Constants;
using DOL.API.Models.Customs.Response;
using DOL.API.Models.PaperGeneration;
using DOL.API.Models.Response;

namespace DOL.API.Services.GenerateDocument
{
    public class SiteNetworkPaper
    {
        public SiteNetworkPaper()
        {
        }

        public async Task<Response> GetnerateDocument(SiteInformationResponse param)
        {
            Response result = new Response();

            SiteNetwork1Position network1Position = new SiteNetwork1Position();
            SiteNetworkOtherPosition networkOtherPosition = new SiteNetworkOtherPosition();


            try
            {
                if (param != null)
                {

                    if (param.SiteNetworkId == 2)
                    {
                        var getSiteNetworkId = "1";
                        var getSiteNetworkSeq = param.SiteNetworkSeq;
                        var position2Setup = getSiteNetworkId + " (" + getSiteNetworkSeq + ")";

                        network1Position.position1 = param.LocationName;
                        network1Position.position2 = position2Setup;
                        network1Position.position3 = param.ProvinceName;

                        PaperGeneration paper = new PaperGeneration("Site1.jpeg",42);

                        paper.addText(network1Position.position1, 500, 484);
                        paper.addText(network1Position.position2, 1500, 484);
                        paper.addText(network1Position.position3, 1850, 484);

                        byte[] streamResult = paper.getPaper();

                        result.data = streamResult;
                        result.httpCode = Constants.httpCode200;
                        result.message = Constants.httpCode200Message;
                    }

                    else if (param.SiteNetworkId == 3)
                    {
                        var getSiteNetworkId = param.SiteNetworkId == 3 ? "2" : param.SiteNetworkId == 4 ? "3" : param.SiteNetworkId == 5 ? "4" : "";
                        var getSiteNetworkSeq = param.SiteNetworkSeq;
                        var position2Setup = getSiteNetworkId + " (" + getSiteNetworkSeq + ")";

                        networkOtherPosition.position1 = param.LocationName;
                        networkOtherPosition.position2 = position2Setup;
                        networkOtherPosition.position3 = param.ProvinceName;

                        PaperGeneration paper = new PaperGeneration("Site2.jpeg",42);

                        paper.addText(networkOtherPosition.position1, 500, 484);
                        paper.addText(networkOtherPosition.position2, 1550, 484);
                        //paper.addText(networkOtherPosition.position3, 1900, 484);

                        byte[] streamResult = paper.getPaper();

                        result.status = false;
                        result.data = streamResult;
                        result.httpCode = Constants.httpCode200;
                        result.message = Constants.httpCode200Message;
                    }

                    else if (param.SiteNetworkId == 4 || param.SiteNetworkId == 5)
                    {
                        var getSiteNetworkId = param.SiteNetworkId == 3 ? "2" : param.SiteNetworkId == 4 ? "3" : param.SiteNetworkId == 5 ? "4" : "";
                        var getSiteNetworkSeq = param.SiteNetworkSeq;
                        var position2Setup = getSiteNetworkId + " (" + getSiteNetworkSeq + ")";

                        networkOtherPosition.position1 = param.LocationName;
                        networkOtherPosition.position2 = position2Setup;
                        networkOtherPosition.position3 = param.ProvinceName;

                        PaperGeneration paper = new PaperGeneration("Site3-4.jpeg",42);

                        paper.addText(networkOtherPosition.position1, 500, 484);
                        paper.addText(networkOtherPosition.position2, 1450, 484);
                        //paper.addText(networkOtherPosition.position3, 1900, 484);

                        byte[] streamResult = paper.getPaper();

                        result.status = false;
                        result.data = streamResult;
                        result.httpCode = Constants.httpCode200;
                        result.message = Constants.httpCode200Message;
                    }
                    else
                    {
                        result.status = false;
                        result.message = "ไม่สามารถพิมพ์เอกสารได้ เนื่องจากเป็นหน่วยงานส่วนกลาง";
                        result.httpCode = Constants.httpCode400;
                    }
                }
                else
                {
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

