using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Nop.Core;
using Nop.Core.Infrastructure;
using Nop.Services.Customers;
using Nop.Services.Media.RoxyFileman;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Areas.Admin.Models.Catalog;
using Microsoft.AspNetCore.Hosting;

namespace Nop.Web.Areas.Admin.Controllers
{
    //Controller for Roxy fileman (http://www.roxyfileman.com/) for TinyMCE editor
    //the original file was \RoxyFileman-1.4.5-net\fileman\asp_net\main.ashx

    //do not validate request token (XSRF)
    public class RoxyFilemanController : BaseAdminController
    {
        #region Fields

        private readonly IPermissionService _permissionService;
        private readonly IRoxyFilemanService _roxyFilemanService;
        private readonly IWorkContext _workContext;
        private readonly ICustomerService _customerService;
        protected readonly INopFileProvider _fileProvider;
        private readonly IStoreContext _storeContext;
        protected readonly IWebHostEnvironment _webHostEnvironment;
        
        #endregion

        #region Ctor

        public RoxyFilemanController(
            IPermissionService permissionService,
            IRoxyFilemanService roxyFilemanService,
            ICustomerService customerService,
             INopFileProvider fileProvider,
            IWorkContext workContext,
            IStoreContext storeContext,
            IWebHostEnvironment webHostEnvironment)
        {
            _permissionService = permissionService;
            _roxyFilemanService = roxyFilemanService;
            _customerService = customerService;
            _workContext = workContext;
            _fileProvider = fileProvider;
            _storeContext = storeContext;
            _webHostEnvironment = webHostEnvironment;
        }

        string _productName = "";
        #endregion

        #region Methods

        /// <summary>
        /// Create configuration file for RoxyFileman
        /// </summary>
        public virtual async Task CreateConfiguration(string roxyProductName)
        {
            
            await _roxyFilemanService.CreateConfigurationAsync();
            this._productName = roxyProductName;
        }

        /// <summary>
        /// Process request
        /// </summary>
        [IgnoreAntiforgeryToken]
        public virtual void ProcessRequest()
        {
            
            //async requests are disabled in the js code, so use .Wait() method here
            ProcessRequestAsync().Wait();
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Process the incoming request
        /// </summary>
        /// <returns>A task that represents the completion of the operation</returns>
        [IgnoreAntiforgeryToken]
        protected virtual async Task ProcessRequestAsync()
        {

            var str = this._productName;
           //string productName = "testing-roxy";
            var action = "DIRLIST";
            try
            {
                var customer = await _workContext.GetCurrentCustomerAsync();
                if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.HtmlEditorManagePictures))
                    throw new Exception("You don't have required permission");

                if (!StringValues.IsNullOrEmpty(HttpContext.Request.Query["a"]))
                    action = HttpContext.Request.Query["a"];
                
                switch (action.ToUpperInvariant())
                {
                    case "DIRLIST":
                        if (customer.VendorId != 0)
                        {
                             await _roxyFilemanService.GetDirectoriesAsync(HttpContext.Request.Query["type"], HttpContext.Request.Query["productName"]);


                        }
                        else
                        {
                            await _roxyFilemanService.GetDirectoriesAsync(HttpContext.Request.Query["type"]);
                        }
                        break;
                    case "FILESLIST":
                        if (customer.VendorId != 0)
                        { 
                            await _roxyFilemanService.GetFilesAsync("/"+ _productName, HttpContext.Request.Query["type"]);
                        }
                        else
                        {
                            await _roxyFilemanService.GetFilesAsync(HttpContext.Request.Query["d"], HttpContext.Request.Query["type"]);
                        }
                        break;
                    case "COPYDIR":
                        if (customer.VendorId != 0)
                        {
                            await HttpContext.Response.WriteAsync(_roxyFilemanService.GetErrorResponse("You don't have permission to perform this action"));
                        }
                        else
                        {
                            await _roxyFilemanService.CopyDirectoryAsync(HttpContext.Request.Query["d"], HttpContext.Request.Query["n"]);
                        }
                        break;
                    case "COPYFILE":
                        if (customer.VendorId != 0)
                        {
                            await HttpContext.Response.WriteAsync(_roxyFilemanService.GetErrorResponse("You don't have permission to perform this action"));
                        }
                        else
                        {
                            await _roxyFilemanService.CopyFileAsync(HttpContext.Request.Query["f"], HttpContext.Request.Query["n"]);
                        }
                        break;
                    case "CREATEDIR":
                        if (customer.VendorId != 0)
                        {
                            await HttpContext.Response.WriteAsync(_roxyFilemanService.GetErrorResponse("Click on Add file and the file can be added directly"));
                        }
                        else
                        {
                            await _roxyFilemanService.CreateDirectoryAsync(HttpContext.Request.Query["d"], HttpContext.Request.Query["n"]);
                        }
                        break;
                    case "DELETEDIR":
                        if (customer.VendorId != 0)
                        {
                            await HttpContext.Response.WriteAsync(_roxyFilemanService.GetErrorResponse("You don't have permission to perform this action"));
                        }
                        else
                        {
                            await _roxyFilemanService.DeleteDirectoryAsync(HttpContext.Request.Query["d"]);
                        }
                        break;
                    case "DELETEFILE":
                        if (customer.VendorId != 0)
                        {
                            await HttpContext.Response.WriteAsync(_roxyFilemanService.GetErrorResponse("You don't have permission to perform this action"));
                        }
                        else
                        {
                            await _roxyFilemanService.DeleteFileAsync(HttpContext.Request.Query["f"]);
                        }
                        break;
                    case "DOWNLOAD":
                        if (customer.VendorId != 0)
                        {
                            await HttpContext.Response.WriteAsync(_roxyFilemanService.GetErrorResponse("You don't have permission to perform this action"));
                        }
                        else
                        {
                            await _roxyFilemanService.DownloadFileAsync(HttpContext.Request.Query["f"]);
                        }
                        break;
                    case "DOWNLOADDIR":
                        if (customer.VendorId != 0)
                        {
                            await HttpContext.Response.WriteAsync(_roxyFilemanService.GetErrorResponse("You don't have permission to perform this action"));
                        }
                        else
                        {
                            await _roxyFilemanService.DownloadDirectoryAsync(HttpContext.Request.Query["d"]);
                        }
                        break;
                    case "MOVEDIR":
                        if (customer.VendorId != 0)
                        {
                            await HttpContext.Response.WriteAsync(_roxyFilemanService.GetErrorResponse("You don't have permission to perform this action"));
                        }
                        else
                        {
                            await _roxyFilemanService.MoveDirectoryAsync(HttpContext.Request.Query["d"], HttpContext.Request.Query["n"]);
                        }
                        break;
                    case "MOVEFILE":
                        if (customer.VendorId != 0)
                        {
                            await HttpContext.Response.WriteAsync(_roxyFilemanService.GetErrorResponse("You don't have permission to perform this action"));
                        }
                        else
                        {
                            await _roxyFilemanService.MoveFileAsync(HttpContext.Request.Query["f"], HttpContext.Request.Query["n"]);
                        }
                        break;
                    case "RENAMEDIR":
                        if (customer.VendorId != 0)
                        {
                            await HttpContext.Response.WriteAsync(_roxyFilemanService.GetErrorResponse("You don't have permission to perform this action"));
                        }
                        else
                        {
                            await _roxyFilemanService.RenameDirectoryAsync(HttpContext.Request.Query["d"], HttpContext.Request.Query["n"]);
                        }
                        break;
                    case "RENAMEFILE":
                        if (customer.VendorId != 0)
                        {
                            await HttpContext.Response.WriteAsync(_roxyFilemanService.GetErrorResponse("You don't have permission to perform this action"));
                        }
                        else
                        {
                            await _roxyFilemanService.RenameFileAsync(HttpContext.Request.Query["f"], HttpContext.Request.Query["n"]);
                        }
                        break;
                    case "GENERATETHUMB":
                        if (customer.VendorId != 0)
                        {
                            await HttpContext.Response.WriteAsync(_roxyFilemanService.GetErrorResponse("You don't have permission to perform this action"));
                        }
                        else
                        {
                            await _roxyFilemanService.CreateImageThumbnailAsync(HttpContext.Request.Query["f"]);
                        }
                        break;
                    case "UPLOAD":
                        
                        if (customer.VendorId != 0)
                        {
                            if (string.IsNullOrEmpty(_productName))
                            {
                                await HttpContext.Response.WriteAsync(_roxyFilemanService.GetErrorResponse("Please do save the product and continue. "));
                            }
                            if (!_fileProvider.DirectoryExists(_fileProvider.Combine(_webHostEnvironment.WebRootPath, "images/uploaded/",_productName)))
                                await _roxyFilemanService.CreateDirectoryAsync("", _productName);


                                await _roxyFilemanService.UploadFilesAsync("/" + _productName);
                        }
                        else
                        {
                            await _roxyFilemanService.UploadFilesAsync(HttpContext.Request.Form["d"]);
                        }
                        break;
                    default:
                        await HttpContext.Response.WriteAsync(_roxyFilemanService.GetErrorResponse("This action is not implemented."));
                        break;
                }
            }
            catch (Exception ex)
            {
                if (action == "UPLOAD" && !_roxyFilemanService.IsAjaxRequest())
                    await HttpContext.Response.WriteAsync($"<script>parent.fileUploaded({_roxyFilemanService.GetErrorResponse(await _roxyFilemanService.GetLanguageResourceAsync("E_UploadNoFiles"))});</script>");
                else
                    await HttpContext.Response.WriteAsync(_roxyFilemanService.GetErrorResponse(ex.Message));
            }
        }

        #endregion
    }
}