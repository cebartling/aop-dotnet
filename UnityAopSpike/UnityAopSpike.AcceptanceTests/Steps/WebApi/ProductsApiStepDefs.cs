using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using UnityAopSpike.AcceptanceTests.Steps.Core;
using UnityAopSpike.Core.Domain;

namespace UnityAopSpike.AcceptanceTests.Steps.WebApi
{
    [Binding]
    public class ProductsApiStepDefs
    {
        private IList<Product> _products;
        private string _responseContent;
        private HttpStatusCode _statusCode;


        [Given(@"products exist in the system")]
        public void GivenProductsExistInTheSystem()
        {
            _products = TestingDomain.CreateProducts(20);
        }

        [When(@"I attempt to retrieve the products through the appropriate ReST API")]
        public void WhenIAttemptToRetrieveTheProductsThroughTheAppropriateReSTAPI()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("/api/Products").Result;
                _statusCode = response.StatusCode;
                _responseContent = response.Content.ReadAsStringAsync().Result;
            }
        }

        [Then(@"the request should be successful")]
        public void ThenTheRequestShouldBeSuccessful()
        {
            Assert.AreEqual(HttpStatusCode.OK, _statusCode);
        }

        [Then(@"a representation of products should be returned in JSON format")]
        public void ThenARepresentationOfProductsShouldBeReturnedInJSONFormat()
        {
            _products = JsonConvert.DeserializeObject(_responseContent, typeof (IList<Product>)) as IList<Product>;
            Assert.IsNotNull(_products);
            Assert.IsTrue(_products.Count >= 20);
        }
    }
}