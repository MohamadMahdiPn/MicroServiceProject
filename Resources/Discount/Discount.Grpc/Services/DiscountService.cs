using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        #region Constructor
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;
        public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region GetDiscount
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _discountRepository.GetDiscount(request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount With Product {request.ProductName} not found"));
            }

            return _mapper.Map<CouponModel>(coupon);

            //return new CouponModel
            //{
            //    Id = coupon.Id,
            //    ProductName = coupon.ProductName,
            //    Amount = coupon.Amount,
            //    Description = coupon.Description,
            //};
        }
        #endregion

        #region Create Discount
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            await _discountRepository.CreateDiscount(coupon);
            _logger.LogInformation($"discount succeeded Add {coupon.ProductName}");

            return _mapper.Map<CouponModel>(coupon);
        }
        #endregion

        #region Update Discount

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            await _discountRepository.UpdateDiscount(coupon);
            _logger.LogInformation($"discount succeeded Update {coupon.ProductName}");

            return _mapper.Map<CouponModel>(coupon);
        }
        #endregion

        #region Delete Discount

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequset request, ServerCallContext context)
        {
            var deleted = await _discountRepository.DeleteDiscount(request.ProductName);

            return new DeleteDiscountResponse
            {
                Success = deleted,
            };
        }
        #endregion
    }
}
