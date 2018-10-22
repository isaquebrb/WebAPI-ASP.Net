using DevStore.DataAccess.DataContexts;
using DevStore.Domain;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DevStore.Api.Controllers
{
    public class ProductController : ApiController
    {
        private DevStoreDataContext db = new DevStoreDataContext();

        [Route("products")]
        public HttpResponseMessage GetProducts()
        {
            var result = db.Products.Include("Category").ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("categories")]
        public HttpResponseMessage GetGategories()
        {
            var result = db.Categories.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("categories/{categoryId}/products")]
        public HttpResponseMessage GetProductsByCategories(int categoryId)
        {
            var result = db.Products.Include("Category").Where(x => x.CategoryId == categoryId).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("products")]
        public HttpResponseMessage PostProduct(Product product)
        {
            if(product == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                db.Products.Add(product);
                db.SaveChanges();

                var result = product;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao incluir produto");
            }

        }

        [HttpDelete]
        [Route("products")]
        public HttpResponseMessage DeleteProduct(int productId)
        {
            if(productId <= 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                Product productToRemove = new Product();
                productToRemove = db.Products.Find(productId);

                db.Products.Remove(productToRemove);
                db.SaveChanges();

                return Request.CreateResponse($"Produto '{productToRemove.Title}' excluido.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut]
        [Route("products")]
        public HttpResponseMessage PutProducts(Product product)
        {
            if (product == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            try
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                var result = product;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPatch]
        [Route("products")]
        public HttpResponseMessage PatchProduct(Product product)
        {
            if (product == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            try
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                var result = product;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
                db.Dispose();
        }
    }
}