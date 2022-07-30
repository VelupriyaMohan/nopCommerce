using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Vendors;
using Nop.Data;
using Nop.Services.Catalog;
using Nop.Services.Customers;
using Nop.Services.Discounts;
using Nop.Services.Html;
using Nop.Services.Security;
using Nop.Services.Stores;

namespace Nop.Services.Vendors
{
    /// <summary>
    /// Vendor service
    /// </summary>
    public partial class VendorService : IVendorService
    {
        #region Fields

        private readonly IHtmlFormatter _htmlFormatter;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Vendor> _vendorRepository;
        private readonly IRepository<VendorNote> _vendorNoteRepository;


        private readonly IStaticCacheManager _staticCacheManager;
        private readonly ICustomerService _customerService;
        private readonly IStoreContext _storeContext;
        private readonly IRepository<DiscountVendorMapping> _discountVendorMappingRepository;

        //private readonly IAclService _aclService;
        //private readonly IStoreMappingService _storeMappingService;
        //private readonly IRepository<ProductVendor> _productVendorRepository;
        //private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public VendorService(IHtmlFormatter htmlFormatter,
            IRepository<Customer> customerRepository,
            IRepository<Product> productRepository,
            IRepository<Vendor> vendorRepository,
            IRepository<VendorNote> vendorNoteRepository,
            IStaticCacheManager staticCacheManager,
            ICustomerService customerService,
            IStoreContext storeContext,
            IRepository<DiscountVendorMapping> discountVendorMappingRepository
            //,
             //IAclService aclService,
             //IStoreMappingService storeMappingService,
             //IRepository<ProductVendor> productVendorRepository,
             //IWorkContext workContext
            )
        {
            _htmlFormatter = htmlFormatter;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _vendorRepository = vendorRepository;
            _vendorNoteRepository = vendorNoteRepository;
            _staticCacheManager = staticCacheManager;
            _customerService = customerService;
            _storeContext = storeContext;
            _discountVendorMappingRepository = discountVendorMappingRepository;
        //     _aclService = aclService;
        //_storeMappingService = storeMappingService;
        //_productVendorRepository = productVendorRepository;
        //_workContext = workContext;

    }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a vendor by vendor identifier
        /// </summary>
        /// <param name="vendorId">Vendor identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the vendor
        /// </returns>
        public virtual async Task<Vendor> GetVendorByIdAsync(int vendorId)
        {
            return await _vendorRepository.GetByIdAsync(vendorId, cache => default);
        }

        /// <summary>
        /// Gets a vendor by product identifier
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the vendor
        /// </returns>
        public virtual async Task<Vendor> GetVendorByProductIdAsync(int productId)
        {
            if (productId == 0)
                return null;

            return await (from v in _vendorRepository.Table
                          join p in _productRepository.Table on v.Id equals p.VendorId
                          where p.Id == productId
                          select v).FirstOrDefaultAsync();
        }


        /// <summary>
        /// Gets a vendors by vendors identifier
        /// </summary>
        /// <param name="vendorIds">Vendor identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the vendors list
        /// </returns>
        public virtual async Task<IList<Vendor>> GetVendorsByIdsAsync(int[] vendorIds)
        {

            //TODO: cache layer should be added
            if (vendorIds is null)
                throw null;//new ArgumentNullException(nameof(vendorIds));

            return await (from v in _vendorRepository.Table
                          where vendorIds.Contains( v.Id ) && !v.Deleted && v.Active
                          select v).Distinct().ToListAsync();
        }

        /// <summary>
        /// Gets vendors by product identifiers
        /// </summary>
        /// <param name="productIds">Array of product identifiers</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the vendors
        /// </returns>
        public virtual async Task<IList<Vendor>> GetVendorsByProductIdsAsync(int[] productIds)
        {
            if (productIds is null)
                throw new ArgumentNullException(nameof(productIds));

            return await (from v in _vendorRepository.Table
                          join p in _productRepository.Table on v.Id equals p.VendorId
                          where productIds.Contains(p.Id) && !v.Deleted && v.Active
                          select v).Distinct().ToListAsync();
        }

        /// <summary>
        /// Gets a vendors by customers identifiers
        /// </summary>
        /// <param name="customerIds">Array of customer identifiers</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the vendors
        /// </returns>
        public virtual async Task<IList<Vendor>> GetVendorsByCustomerIdsAsync(int[] customerIds)
        {
            if (customerIds is null)
                throw new ArgumentNullException(nameof(customerIds));

            return await (from v in _vendorRepository.Table
                          join c in _customerRepository.Table on v.Id equals c.VendorId
                          where customerIds.Contains(c.Id) && !v.Deleted && v.Active
                          select v).Distinct().ToListAsync();
        }

        /// <summary>
        /// Delete a vendor
        /// </summary>
        /// <param name="vendor">Vendor</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteVendorAsync(Vendor vendor)
        {
            await _vendorRepository.DeleteAsync(vendor);
        }

        /// <summary>
        /// Gets all vendors
        /// </summary>
        /// <param name="name">Vendor name</param>
        /// <param name="email">Vendor email</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the vendors
        /// </returns>
        public virtual async Task<IPagedList<Vendor>> GetAllVendorsAsync(string name = "", string email = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var vendors = await _vendorRepository.GetAllPagedAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(v => v.Name.Contains(name));

                if (!string.IsNullOrWhiteSpace(email))
                    query = query.Where(v => v.Email.Contains(email));

                if (!showHidden)
                    query = query.Where(v => v.Active);

                query = query.Where(v => !v.Deleted);
                query = query.OrderBy(v => v.DisplayOrder).ThenBy(v => v.Name).ThenBy(v => v.Email);

                return query;
            }, pageIndex, pageSize);

            return vendors;
        }

        /// <summary>
        /// Inserts a vendor
        /// </summary>
        /// <param name="vendor">Vendor</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task InsertVendorAsync(Vendor vendor)
        {
            await _vendorRepository.InsertAsync(vendor);
        }

        /// <summary>
        /// Updates the vendor
        /// </summary>
        /// <param name="vendor">Vendor</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task UpdateVendorAsync(Vendor vendor)
        {
            await _vendorRepository.UpdateAsync(vendor);
        }

        /// <summary>
        /// Gets a vendor note
        /// </summary>
        /// <param name="vendorNoteId">The vendor note identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the vendor note
        /// </returns>
        public virtual async Task<VendorNote> GetVendorNoteByIdAsync(int vendorNoteId)
        {
            return await _vendorNoteRepository.GetByIdAsync(vendorNoteId, cache => default);
        }

        /// <summary>
        /// Gets all vendor notes
        /// </summary>
        /// <param name="vendorId">Vendor identifier</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the vendor notes
        /// </returns>
        public virtual async Task<IPagedList<VendorNote>> GetVendorNotesByVendorAsync(int vendorId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _vendorNoteRepository.Table.Where(vn => vn.VendorId == vendorId);

            query = query.OrderBy(v => v.CreatedOnUtc).ThenBy(v => v.Id);

            return await query.ToPagedListAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// Deletes a vendor note
        /// </summary>
        /// <param name="vendorNote">The vendor note</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteVendorNoteAsync(VendorNote vendorNote)
        {
            await _vendorNoteRepository.DeleteAsync(vendorNote);
        }

        /// <summary>
        /// Inserts a vendor note
        /// </summary>
        /// <param name="vendorNote">Vendor note</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task InsertVendorNoteAsync(VendorNote vendorNote)
        {
            await _vendorNoteRepository.InsertAsync(vendorNote);
        }

        /// <summary>
        /// Formats the vendor note text
        /// </summary>
        /// <param name="vendorNote">Vendor note</param>
        /// <returns>Formatted text</returns>
        public virtual string FormatVendorNoteText(VendorNote vendorNote)
        {
            if (vendorNote == null)
                throw new ArgumentNullException(nameof(vendorNote));

            var text = vendorNote.Note;

            if (string.IsNullOrEmpty(text))
                return string.Empty;

            text = _htmlFormatter.FormatText(text, false, true, false, false, false, false);

            return text;
        }


        /// <summary>
        /// Get vendor identifiers to which a discount is applied
        /// </summary>
        /// <param name="discount">Discount</param>
        /// <param name="customer">Customer</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the vendor identifiers
        /// </returns>
        public virtual async Task<IList<int>> GetAppliedVendorIdsAsync(Discount discount, Customer customer)
        {
            if (discount == null)
                throw new ArgumentNullException(nameof(discount));

            var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(NopDiscountDefaults.VendorIdsByDiscountCacheKey,
                discount,
                await _customerService.GetCustomerRoleIdsAsync(customer),
                await _storeContext.GetCurrentStoreAsync());

           var query = _discountVendorMappingRepository.Table.Where(dmm => dmm.DiscountId == discount.Id)
                .Select(dmm => dmm.EntityId);

            var result = await _staticCacheManager.GetAsync(cacheKey, async () => await query.ToListAsync());

            return result;
        }


        /// <summary>
        /// Clean up vendor references for a specified discount
        /// </summary>
        /// <param name="discount">Discount</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task ClearDiscountVendorMappingAsync(Discount discount)
        {
            if (discount is null)
                throw new ArgumentNullException(nameof(discount));

            var mappings = _discountVendorMappingRepository.Table.Where(dcm => dcm.DiscountId == discount.Id);

            await _discountVendorMappingRepository.DeleteAsync(mappings.ToList());
        }


        /// <summary>
        /// Get a discount-vendor mapping record
        /// </summary>
        /// <param name="vendorId">Vendor identifier</param>
        /// <param name="discountId">Discount identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the result
        /// </returns>
        public async Task<DiscountVendorMapping> GetDiscountAppliedToVendorAsync(int vendorId, int discountId)
        {
            return await _discountVendorMappingRepository.Table
                .FirstOrDefaultAsync(dcm => dcm.EntityId == vendorId && dcm.DiscountId == discountId);
        }

        /// <summary>
        /// Inserts a discount-vendor mapping record
        /// </summary>
        /// <param name="discountVendorMapping">Discount-vendor mapping</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task InsertDiscountVendorMappingAsync(DiscountVendorMapping discountVendorMapping)
        {
            await _discountVendorMappingRepository.InsertAsync(discountVendorMapping);
        }

        /// <summary>
        /// Deletes a discount-vendor mapping record
        /// </summary>
        /// <param name="discountVendorMapping">Discount-vendor mapping</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task DeleteDiscountVendorMappingAsync(DiscountVendorMapping discountVendorMapping)
        {
            await _discountVendorMappingRepository.DeleteAsync(discountVendorMapping);
        }


        /// <summary>
        /// Get vendors for which a discount is applied
        /// </summary>
        /// <param name="discountId">Discount identifier; pass null to load all records</param>
        /// <param name="showHidden">A value indicating whether to load deleted vendors</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the list of vendors
        /// </returns>
        public virtual async Task<IPagedList<Vendor>> GetVendorsWithAppliedDiscountAsync(int? discountId = null,
            bool showHidden = false, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var vendors = _vendorRepository.Table;

            if (discountId.HasValue)
                vendors = from vendor in vendors
                          join dmm in _discountVendorMappingRepository.Table on vendor.Id equals dmm.EntityId
                          where dmm.DiscountId == discountId.Value
                          select vendor;

            if (!showHidden)
                vendors = vendors.Where(vendor => !vendor.Deleted);

            vendors = vendors.OrderBy(vendor => vendor.DisplayOrder).ThenBy(vendor => vendor.Id);

            return await vendors.ToPagedListAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// Gets a product vendor mapping collection
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the product vendor mapping collection
        /// </returns>
        //public virtual async Task<IList<ProductVendor>> GetProductVendorsByProductIdAsync(int productId,
        //    bool showHidden = false)
        //{

        //    //TODO: edited random - cleanup needed
        //    if (productId == 0)
        //        return new List<ProductVendor>();

        //   // return new List<ProductVendor>();

        //    var store = await _storeContext.GetCurrentStoreAsync();
        //    var customer = await _workContext.GetCurrentCustomerAsync();

        //    var key = _staticCacheManager
        //        .PrepareKeyForDefaultCache(NopCatalogDefaults.ProductVendorsByProductCacheKey, productId, showHidden, customer, store);

        //    var query = from pm in _productVendorRepository.Table
        //                join m in _vendorRepository.Table on pm.VendorId equals m.Id
        //                where pm.ProductId == productId && !m.Deleted
        //                orderby pm.DisplayOrder, pm.Id
        //                select pm;

        //    if (!showHidden)
        //    {
        //        var vendorsQuery = _vendorRepository.Table.Where(m => m.IsWholeCitySupply);
        //            //.Where(m => m.Published);

        //        //apply store mapping constraints
        //       // vendorsQuery = await _storeMappingService.ApplyStoreMapping(vendorsQuery, store.Id);

        //        //apply ACL constraints
        //       // vendorsQuery = await _aclService.ApplyAcl(vendorsQuery, customer);

        //        query = query.Where(pm => vendorsQuery.Any(m => m.Id == pm.VendorId));
        //    }

        //    return await _staticCacheManager.GetAsync(key, query.ToList);
        //}
        #endregion
    }
}

