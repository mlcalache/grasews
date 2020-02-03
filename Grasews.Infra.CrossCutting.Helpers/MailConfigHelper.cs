//using System;
//using System.Configuration;
//using System.Xml;

//namespace Grasews.Infra.CrossCutting.Helpers
//{
//    public class MailConfigHelper : IConfigurationSectionHandler
//    {
//        [ConfigurationProperty("mailAddress", IsRequired = true)]
//        public string NomeUsuario { get; set; }

//        [ConfigurationProperty("smtpPort", IsRequired = true)]
//        public int Porta { get; set; }

//        [ConfigurationProperty("smtpPassword", IsRequired = true)]
//        public string Senha { get; set; }

//        [ConfigurationProperty("smtpHost", IsRequired = true)]
//        public string Servidor { get; set; }

//        [ConfigurationProperty("smtpEnablessl", IsRequired = true)]
//        public bool SSL { get; set; }

//        public object Create(object parent, object configContext, XmlNode section)
//        {
//            Servidor = section.Attributes["smtpHost"].Value;
//            Porta = int.Parse(section.Attributes["smtpPort"].Value);
//            SSL = Convert.ToBoolean(section.Attributes["smtpEnablessl"].Value);
//            Senha = section.Attributes["smtpPassword"].Value;
//            NomeUsuario = section.Attributes["mailAddress"].Value;
//            return this;
//        }
//    }
//}