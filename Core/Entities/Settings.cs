using Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Settings
    {
        #region MongoDB
        private string _mongoDbConnection;

        public string MONGODB_CONNECTION
        {
            get => _mongoDbConnection;
            set { if (!string.IsNullOrEmpty(value)) _mongoDbConnection = Encryptor.Instance.Decrypt(value); }
        }

        public string MongoDb { get; set; } = "WatchList";
        public bool MongoIsSsl { get; set; } = false;
        #endregion
        public string ENVIRONMENT { get; set; } = "PROD";
        public string ASPNETCORE_ENVIRONMENT { get; set; }
    }
}
