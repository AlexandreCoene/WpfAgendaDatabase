﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAgendaDatabase.Service.DAO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;


namespace WpfAgendaDatabase.Service.API
{
    public class Installed
    {
        public string client_id { get; set; }
        public string project_id { get; set; }
        public string auth_uri { get; set; }
        public string token_uri { get; set; }
        public string auth_provider_x509_cert_url { get; set; }
        public string client_secret { get; set; }
        public List<string> redirect_uris { get; set; }
    }

    public class Root
    {
        public Installed installed { get; set; }
    }
}
