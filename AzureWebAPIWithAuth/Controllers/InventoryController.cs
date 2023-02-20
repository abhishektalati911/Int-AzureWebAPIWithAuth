using AzureWebAPIWithAuth.Models;
using AzureWebAPIWithAuth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AzureWebAPIWithAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryService inventoryService;

        /// <summary>
        /// structor Injection (Dependancy Injection)
        /// </summary>
        /// <param name="inventoryService"></param>
        /// <param name="logger"></param>
        public InventoryController(InventoryService inventoryService)
        {
            this.inventoryService = inventoryService;
            
        }

        /// <summary>
        /// Get All Records From Inventory 
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        // Token Check 
        [Authorize]
        [Route("InventoryDetails")]
        public async Task<IActionResult> GetInventory()
        {
            try
            {
                return Ok(await inventoryService.GetInventoryList());
            }
            catch (Exception ex)
            {
                Log.Error("Controller Name : " + this.ControllerContext.ActionDescriptor.ControllerName + "Controller" +
                    " \nFailure message : " + ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Insert Into Inventory
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        
        [HttpPost]
        [Authorize]
        [Route("AddInventory")]
        public async Task<IActionResult> AddInventory(Inventory inventory)
        {
            try
            {
                if (inventory == null)
                {
                    ModelState.AddModelError("inventory", "inventory model error");
                    return BadRequest(ModelState);
                }

                int result = await inventoryService.AddInventory(inventory);
                if (result > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Controller Name : " + this.ControllerContext.ActionDescriptor.ControllerName + "Controller" +
                    " \nFailure message : " + ex);
                return StatusCode(StatusCodes.Status500InternalServerError,
                                        ex);
            }
        }


        /// <summary>
        /// Update Records In Inventory
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        
        [HttpPut]
        [Authorize]
        [Route("UpdateInventory")]
        public async Task<IActionResult> UpdateInventory(Inventory inventory)
        {
            try
            {
                if (inventory == null)
                {
                    ModelState.AddModelError("inventory", "inventory model error");
                    return BadRequest(ModelState);
                }
                int result = await inventoryService.UpdateInventory(inventory);
                if (result > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Controller Name : " + this.ControllerContext.ActionDescriptor.ControllerName + "Controller" +
                    " \nFailure message : " + ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        /// <summary>
        /// Delete Record From Inventory
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// 
        [HttpDelete]
        [Authorize]
        [Route("DeleteInventory")]

       
        public async Task<IActionResult> DeleteInventory(int ID)
        {
            try
            {
                if (ID == 0)
                {
                    ModelState.AddModelError("ID", "ID model error");
                    return BadRequest(ModelState);
                }
                int result = await inventoryService.DeleteInventory(ID);
                if (result > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Controller Name : " + this.ControllerContext.ActionDescriptor.ControllerName + "Controller" +
                    " \nFailure message : " + ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
