﻿using Application.Client.Commands.CreateClient;
using Application.Client.Commands.DeleteClient;
using Application.Client.Commands.UpdateClient;
using Application.Client.Queries.GetAllClient;
using Application.Client.Queries.GetClientById;
using Application.Client.Queries.GetClientByName;
using Application.DTOs.Client;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;




namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,User")]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]       
        public async Task<IActionResult> GetAllClients()
        {
            var clientQuery = new GetAllClientQuery();
            var clients = await _mediator.Send(clientQuery);
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var query = new GetClientByIdQuery()
            {
                Id = id
            };
            var client = await _mediator.Send(query);
            if (client != null)
            {
                return Ok(client);
            }
            return NotFound($"Client {id} not found!");
        }
        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientDto createClientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createClientCommand = new CreateClientCommand { ClientDto = createClientDto };

            try
            {
                var createdClient = await _mediator.Send(createClientCommand);

                if (createdClient == null)
                {
                    // Handle the case where the createdClient is null
                    return StatusCode(500, "Internal server error: Unable to create the client");
                }
                return CreatedAtAction(nameof(GetAllClients), createdClient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient( [FromBody] UpdateClientDto updateClientDto)
        {
            try
            {
                // Ensure the provided clientId matches the one in the request body
                //if (clientId != updateClientDto.ClientId)
                //{
                //    return BadRequest("Client IDs do not match.");
                //}

                // Create an instance of the UpdateClientCommand
                var updateCommand = new UpdateClientCommand
                {
                    UpdateClientDto = updateClientDto
                };

                // Send the command to the mediator for handling
                var updatedClient = await _mediator.Send(updateCommand);

                if (updatedClient != null)
                {
                    // Return a 200 OK response with the updated client DTO
                    return Ok(updatedClient);
                }
                else
                {
                    // If the update failed or the client was not found, return a 404 Not Found
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return a 500 Internal Server Error response
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while updating the client.");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var command = new DeleteClientCommand() 
            {
                ClientId = id
            };
            var result = await _mediator.Send(command);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound($"Client {id} not found!");
        }

        




        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetClientByName([FromQuery] string name)
        {
            try
            {
                string firstName = name.Split(' ')[0];
                string lastName = name.Split(' ')[1];
                var query = new GetClientByNameQuery()
                {
                    FirstName = firstName,
                    LastName = lastName
                };

                var client = await _mediator.Send(query);

                if (client != null)
                {
                    return Ok(client);
                }
                else
                {
                    return NotFound($"Client {name} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing the request: {ex.Message}");
            }
        }
    }
}
