﻿using System;
namespace DOL.WEBAPP.Models.Request
{
	public class UpdateImageNameRequest
    {
		public UpdateImageNameRequest()
		{
		}

        public int Id { get; set; }

        public string Flag { get; set; }

        public string Username { get; set; }

        public string Filename { get; set; }
    }
}

