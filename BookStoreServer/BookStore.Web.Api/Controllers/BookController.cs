using AutoMapper;
using BookStore.MessageQueue;
using BookStore.RedisMessageQueue.Models;
using BookStore.Web.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BookStore.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IMapper _mapper;
        
        private readonly IPublisher<PurchaseBookRedisModel, long> _producer;

        public BookController(ILogger<BookController> logger,
            IMapper mapper,            
            IPublisher<PurchaseBookRedisModel, long> producer)
        {
            _logger = logger;
            _mapper = mapper;            
            _producer = producer;
        }

        /// <summary>
        /// buy book
        /// </summary>
        /// <param name="model"></param>        
        [HttpPost]
        [Route("BuyBook")]
        public JsonResult BuyBook([FromBody] BookItem model)
        {
            _logger.LogInformation($"Reveive buy book request");
            
            var redisData = _mapper.Map<PurchaseBookRedisModel>(model);
            _producer.PublishAsync(redisData);

            return new JsonResult(new { Message = "Success" });
        }

        [HttpGet]
        [Route("GetBooks")]
        public async Task<IEnumerable<BookItem>> GetBooks(string query)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Hard-code for quick demostration, it should be separated into services layer
                HttpResponseMessage response = await client.GetAsync("https://www.googleapis.com/books/v1/volumes?q={" + query + "}");
                if (response.IsSuccessStatusCode)
                {
                    var bookStr = await response.Content.ReadAsStringAsync();
                    var rootObject = JsonConvert.DeserializeObject<RootObject>(bookStr);
                    return rootObject.Items;
                }
                else
                {
                    throw new Exception("Can not get books data from www.googleapis.com");
                }
            }
        }
    }
}