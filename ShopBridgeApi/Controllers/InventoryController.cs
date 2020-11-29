
using ShopBridgeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopBridge.Api.Controllers
{
    public class InventoryController : ApiController
    {

        [HttpPost, ActionName("Add")]
        public HttpResponseMessage CreateInventory(Inventory inventory)
        {
            HttpResponseMessage result;
            if (ModelState.IsValid)
            {
                using (ShopBridgeEntities db = new ShopBridgeEntities())
                {
                    db.Inventories.Add(inventory);
                    db.SaveChanges();
                }
                result = Request.CreateResponse(HttpStatusCode.Created, inventory);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest, "Server failed to save data");
            }
            return result;
        }

        [HttpGet, ActionName("GetAll")]

        public HttpResponseMessage GetInventoryList()
        {
            HttpResponseMessage result;
            List<Inventory> lstinventorie = new List<Inventory>();

            using (ShopBridgeEntities db = new ShopBridgeEntities())
            {
                lstinventorie = db.Inventories.ToList();
            }
            result = Request.CreateResponse(HttpStatusCode.OK, lstinventorie);


            return result;
        }

        [ActionName("Remove")]
        public HttpResponseMessage RemoveInventory(int ? Id)
        {
            int id = Convert.ToInt32(Id);
            if (Id <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            using (ShopBridgeEntities db = new ShopBridgeEntities())
            {
                var Inventory = db.Inventories.SingleOrDefault(m => m.Id.Equals(id));
                db.Inventories.Remove(Inventory);
                db.SaveChanges();

            }
            return Request.CreateResponse(HttpStatusCode.OK,id);
        }

        [HttpPost]
        public IHttpActionResult GetInventoryById(dynamic id)
        {
            Inventory inventory = null;
            int Id = Convert.ToInt32(id);

            using (ShopBridgeEntities db = new ShopBridgeEntities())
            {

                inventory = db.Inventories.SingleOrDefault(m => m.Id.Equals(Id));
            }

            if (inventory == null)
            {
                return NotFound();
            }

            return Ok(inventory);
        }
    }
}
