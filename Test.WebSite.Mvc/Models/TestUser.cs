﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.WebSite.Mvc
{
    public class TestUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int NumberOfChildren { get; set; }
        public TestAddress Address { get; set; }
    }

    public class TestAddress
    {
        public int Number { get; set; }
        public string Street { get; set; }
        public string Postcode { get; set; }
    }
}