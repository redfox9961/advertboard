using BusinessLogic.Model.Dtos;
using BusinessLogicLayer.Interfaces;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace AdvertBoardUI.Controllers
{
    public class AdvertsController : ApiController
    {
        private IAdvertService _advertService;

        public AdvertsController(IAdvertService advertService)
        {
            _advertService = advertService;
        }

        // GET api/adverts
        [HttpGet]
        [ResponseType(typeof(List<AdvertDto>))]
        public IHttpActionResult GetAll()
        {
            var adverts = _advertService.GetAllAdverts();
            return Ok(adverts);
        }

        // GET api/adverts/5
        [HttpGet]
        [ResponseType(typeof(AdvertDto))]
        public IHttpActionResult Get(int id)
        {
            var advert = _advertService.GetById(id);
            return Ok(advert);
        }
        
        // POST api/adverts
        [HttpPost]
        public IHttpActionResult Save([FromBody]AdvertDto advert)
        {
            _advertService.SaveAdvert(advert);
            return Ok();
        }

        // DELETE api/adverts/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _advertService.DeleteAdvert(id);
            return Ok();
        }
    }
}