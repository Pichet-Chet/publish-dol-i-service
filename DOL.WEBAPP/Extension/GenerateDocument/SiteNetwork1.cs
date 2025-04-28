using System;
using DOL.WEBAPP.Models.PaperGeneration;
using DOL.WEBAPP.Models.Response;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DOL.WEBAPP.Extension.GenerateDocument
{
    public class SiteNetwork1
    {
        public SiteNetwork1()
        {
        }

        public async Task<Response> GetnerateDocument(SiteInformationResponse param)
        {
            Response result = new Response();

            SiteNetwork1Position position = new SiteNetwork1Position();

            try
            {
                if (param != null)
                {
                    #region Assign Data before drawing image

                    position.position1 = "";
                    position.position2 = "";
                    position.position3 = "";
                    
                    #endregion

                    PaperGeneration paper = new PaperGeneration("SiteNetwork1.jpg");

                    paper.addText(position.position1, 135, 100);
                    paper.addText(position.position2, 370, 160);
                    paper.addText(position.position3, 220, 175);
                    
                    byte[] streamResult = paper.getPaper();

                    result.data = streamResult;

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

