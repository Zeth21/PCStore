using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PCStore.Application.Features.CQRSDesignPattern.Commands.CouponCommands;
using PCStore.Application.Features.CQRSDesignPattern.Results;
using PCStore.Application.Features.CQRSDesignPattern.Results.CouponResults;
using PCStore.Domain.Entities;
using PCStore.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCStore.Application.Features.CQRSDesignPattern.Handlers.CouponHandlers
{
    public class CreateCouponHandler(ProjectDbContext context, IMapper mapper) : IRequestHandler<CreateCouponCommand, TaskResult<CreateCouponResult>>
    {

        private readonly ProjectDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        public async Task<TaskResult<CreateCouponResult>> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
        {
            var checkCoupon = await _context.Coupons
                .AnyAsync(x => x.CouponCode == request.CouponCode);
            if (checkCoupon)
                return TaskResult<CreateCouponResult>.Fail("Coupon code already taken!");
            var newCoupon = _mapper.Map<Coupon>(request);
            try
            {
                await _context.Coupons.AddAsync(newCoupon,cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch(Exception ex) 
            {
                return TaskResult<CreateCouponResult>.Fail("Something went wrong! " + ex.Message);
            }
            var result = _mapper.Map<CreateCouponResult>(newCoupon);
            return TaskResult<CreateCouponResult>.Success("Coupon created successfully!", data: result);
        }
    }
}
