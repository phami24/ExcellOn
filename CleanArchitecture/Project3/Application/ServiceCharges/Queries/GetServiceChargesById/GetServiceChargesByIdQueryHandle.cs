﻿using Application.DTOs.ServiceCharges;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceCharges.Queries.GetServiceChargesById
{
    public class GetServiceChargesByIdQueryHandle : IRequestHandler<GetServiceChargesByIdQuery, GetServiceChargesDto>
    {
        private readonly IServiceChargesRepository _serviceChargesRepository;

        public GetServiceChargesByIdQueryHandle(IServiceChargesRepository serviceChargesRepository)
        {
            _serviceChargesRepository = serviceChargesRepository;
        }

        public async Task<GetServiceChargesDto> Handle(GetServiceChargesByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var sc = await _serviceChargesRepository.GetById(request.ServiceChargesId);

                if (sc == null)
                {
                    return null;
                }

                GetServiceChargesDto serviceChargesDto = new GetServiceChargesDto
                {
                    ServiceChargesId = sc.ServiceChargesId,
                    ServiceChargesName = sc.ServiceChargesName,
                    Price = sc.Price,
                    ServiceChargesDescription = sc.ServiceChargesDescription,
                    ServiceId = sc.ServiceId,
                };

                return serviceChargesDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Query : " + ex);
                return null;
            }
        }
    }
}
