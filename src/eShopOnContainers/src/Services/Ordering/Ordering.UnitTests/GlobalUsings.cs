﻿global using System;
global using Microsoft.eShopOnContainers.Services.Ordering.API.Application.Models;
global using MediatR;
global using Microsoft.eShopOnContainers.Services.Ordering.API.Application.Commands;
global using Microsoft.eShopOnContainers.Services.Ordering.Infrastructure.Idempotency;
global using Microsoft.Extensions.Logging;
global using Moq;
global using System.Collections.Generic;
global using System.Threading.Tasks;
global using Xunit;
global using Microsoft.eShopOnContainers.Services.Ordering.API.Infrastructure.Services;
global using Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.BuyerAggregate;
global using Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.OrderAggregate;
global using System.Threading;
global using Ordering.API.Application.IntegrationEvents;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.eShopOnContainers.Services.Ordering.API.Application.Queries;
global using Microsoft.eShopOnContainers.Services.Ordering.API.Controllers;
global using System.Linq;
global using Microsoft.eShopOnContainers.Services.Ordering.Domain.SeedWork;
global using Microsoft.eShopOnContainers.Services.Ordering.Domain.Exceptions;
global using Microsoft.eShopOnContainers.Services.Ordering.Domain.Events;
global using UnitTest.Ordering;
