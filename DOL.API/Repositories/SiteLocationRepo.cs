//using System;
//using DOL.API.Models;
//using DOL.API.Models.Filters;
//using DOL.API.Models.Response;
//using DOL.API.Repositories.Interface;
//using DOL.API.Services;

//namespace DOL.API.Repositories
//{
//	public class SiteLocationRepo : ISiteLocationRepo
//    {
//        private readonly DolContext _chmContext;

//        private readonly SiteLocationService service;

//        public SiteLocationRepo()
//        {
//            _chmContext = new DolContext();

//            service = new SiteLocationService(_chmContext);
//        }

//        public async Task<Response> Get(SiteLocationFilter param)
//        {
//            Response resp = new Response();

//            resp = await Task.Run(() => service.Get(param));

//            return resp;
//        }

//        public async Task<Response> Detail(int id)
//        {
//            Response resp = new Response();

//            resp = await Task.Run(() => service.Detail(id));

//            return resp;
//        }

//        public async Task<Response> Create(SiteLocation param)
//        {
//            Response resp = new Response();

//            resp = await Task.Run(() => service.Create(param));

//            return resp;
//        }


//        public async Task<Response> Update(SiteLocation param)
//        {
//            Response resp = new Response();

//            resp = await Task.Run(() => service.Update(param));

//            return resp;
//        }

//        public async Task<Response> Province()
//        {
//            Response resp = new Response();

//            resp = await Task.Run(() => service.Province());

//            return resp;
//        }

//        public async Task<Response> Location(string provinceName)
//        {
//            Response resp = new Response();

//            resp = await Task.Run(() => service.Location(provinceName));

//            return resp;
//        }
//    }
//}

