using LogisticsHelpSystemLibrary.Models.Api;
using LogisticsHelpSystemLibrary.Models.Database.ApplicationDb;
using LogisticsHelpSystemLibrary.Models.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderLogisticsManagerApplication.Areas.Api.Controllers
{
    [ApiKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class PackingMaterialController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;

        public PackingMaterialController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        // GET: api/<PackingMaterialController>
        [HttpGet]
        public IEnumerable<ApiPackingMaterialModel> Get()
        {
            List<ApiPackingMaterialModel> returnList = new();

            foreach (var packingMaterial in applicationDbContext.PackingMaterials)
            {
                returnList.Add(new ApiPackingMaterialModel()
                {
                    MaterialID = packingMaterial.MaterialID,
                    MaterialPartNumber = packingMaterial.MaterialPartNumber,
                    MaterialName = packingMaterial.MaterialName,
                    HasDimension = packingMaterial.HasDimension,
                    MaterialDepth = packingMaterial.MaterialDepth,
                    MaterialHeigth = packingMaterial.MaterialHeigth,
                    MaterialWeigth = packingMaterial.MaterialWeigth,
                    MaterialWidth = packingMaterial.MaterialWidth
                });
            }

            return returnList;
        }

        // GET api/<PackingMaterialController>/5
        [HttpGet("{id}")]
        public ApiPackingMaterialModel Get(int id)
        {
            var packingMaterial = applicationDbContext.PackingMaterials.Where(x => x.MaterialID == id).FirstOrDefault();

            return new ApiPackingMaterialModel()
            {
                MaterialID = packingMaterial.MaterialID,
                MaterialPartNumber = packingMaterial.MaterialPartNumber,
                MaterialName = packingMaterial.MaterialName,
                HasDimension = packingMaterial.HasDimension,
                MaterialDepth = packingMaterial.MaterialDepth,
                MaterialHeigth = packingMaterial.MaterialHeigth,
                MaterialWeigth = packingMaterial.MaterialWeigth,
                MaterialWidth = packingMaterial.MaterialWidth
            };
        }

        // POST api/<PackingMaterialController>
        [HttpPost]
        public IActionResult Post([FromBody] ApiPackingMaterialModel value)
        {
            if (applicationDbContext.PackingMaterials.Where(x => x.MaterialPartNumber == value.MaterialPartNumber).Any())
                return BadRequest($"PackingMaterial Part Number already exist - with InputValue: {value.MaterialPartNumber}");

            applicationDbContext.Add(new PackingMaterial()
            {
                MaterialID = value.MaterialID,
                MaterialPartNumber = value.MaterialPartNumber,
                MaterialName = value.MaterialName,
                HasDimension = value.HasDimension,
                MaterialDepth = value.MaterialDepth,
                MaterialHeigth = value.MaterialHeigth,
                MaterialWeigth = value.MaterialWeigth,
                MaterialWidth = value.MaterialWidth
            });

            applicationDbContext.SaveChanges();

            return Ok();
        }

        // PUT api/<PackingMaterialController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ApiPackingMaterialModel value)
        {
            if (!applicationDbContext.PackingMaterials.Where(x => x.MaterialID == id).Any())
                return BadRequest($"PackingMaterial does not exist - with InputValue: {value.MaterialID}");

            var packingMaterial = applicationDbContext.PackingMaterials.Where(x => x.MaterialID == id).FirstOrDefault();

            packingMaterial.MaterialID = value.MaterialID;
            packingMaterial.MaterialPartNumber = value.MaterialPartNumber;
            packingMaterial.MaterialName = value.MaterialName;
            packingMaterial.HasDimension = value.HasDimension;
            packingMaterial.MaterialDepth = value.MaterialDepth;
            packingMaterial.MaterialHeigth = value.MaterialHeigth;
            packingMaterial.MaterialWeigth = value.MaterialWeigth;
            packingMaterial.MaterialWidth = value.MaterialWidth;

            applicationDbContext.Update(packingMaterial);
            applicationDbContext.SaveChanges();

            return Ok();
        }

        // DELETE api/<PackingMaterialController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!applicationDbContext.PackingMaterials.Where(x => x.MaterialID == id).Any())
                return BadRequest($"PackingMaterial does not exist - with InputValue: {id}");

            var packingMaterial = applicationDbContext.PackingMaterials.Where(x => x.MaterialID == id).FirstOrDefault();

            if (packingMaterial.packingMaterialUsedOnOrders.Count == 0)
            {
                applicationDbContext.Remove(packingMaterial);
                applicationDbContext.SaveChanges();
            }
            else
                return BadRequest("The PackingMaterial has orders that uses it, therefore is not deleted.");

            return Ok();
        }
    }
}
