﻿using CircuitBreaker.Model;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CircuitBreaker.Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        static int counter = 0;

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new Message
            {
                Id = 1,
                Text = "The request has done successfully"
            });
        }

        [HttpGet]
        public IActionResult Odd()
        {
            counter += 1;

            if (counter % 2 != 0)
            {
                return Ok(new Message
                {
                    Id = counter,
                    Text = "درخواست در دفعات فرد با موفقیت انجام شد"
                });
            }

            return BadRequest(new Message
            {
                Id = counter,
                Text = "به علت زوج بودن شماره درخواست عملیات ناموفق بود"
            });
        }

        [HttpGet]
        public IActionResult Ten()
        {
            counter += 1;

            if (counter % 10 == 0)
            {
                return Ok(new Message
                {
                    Id = counter,
                    Text = "درخواست در دفعات ضریب ۱۰ با موفقیت انجام شد"
                });
            }

            return BadRequest(new Message
            {
                Id = counter,
                Text = "به علت ضریب ۱۰ نبودن شماره درخواست عملیات ناموفق بود"
            });
        }

        [HttpGet]
        public IActionResult Long()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            System.Threading.Thread.Sleep(20000);
            stopwatch.Stop();

            return Ok(new Message
            {
                Id = 3,
                Text = $"درخواست طولانی مدت انجام شد. زمان انجام کار {stopwatch.ElapsedMilliseconds / 1000} ثانیه"
            });
        }

        [HttpGet]
        public IActionResult Ex()
        {
            throw new Exception("Exception in Application");
            return Ok(new Message
            {
                Id = 1,
                Text = "درخواست با موفقیت انجام شد"
            });
        }
    }
}