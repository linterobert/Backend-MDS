﻿using Linte_Robert_proiect_231.Entities;
using Linte_Robert_proiect_231.Entities.DTOs;
using Linte_Robert_proiect_231.Repositories.Cos_ProdusRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Linte_Robert_proiect_231.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CosProdusController : ControllerBase
    {
        private readonly ICosProdusRepository _repository;
        public CosProdusController(ICosProdusRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetAllCosProdusByIdCos(int id)
        {
            var comenzi = await _repository.GetAllCosProdusByUserId(id);

            var comenziToReturn = new List<CosProdusDTO>();

            foreach (var comanda in comenzi)
            {
                comenziToReturn.Add(new CosProdusDTO(comanda));

            }
            return Ok(comenziToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCosProdus()
        {
            var produse = _repository.GetAll();

            var produseToReturn = new List<CosProdusDTO>();

            foreach (var produs in produse)
            {
                produseToReturn.Add(new CosProdusDTO(produs));
            }
            return Ok(produseToReturn);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteCosProdus(int id)
        {
            var comanda = await _repository.GetByIdAsync(id);

            if (comanda == null)
            {
                return NotFound("Comanda nu exista!");
            }

            _repository.Delete(comanda);

            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCosProdus(CreateCosProdusDTO dto)
        {
            Cosprodus newcomanda = new Cosprodus();

            newcomanda.ProdusId = dto.ProdusId;
            newcomanda.UserId = dto.UserId;
            newcomanda.Cantitate = dto.Cantitate;

            _repository.Create(newcomanda);
            await _repository.SaveAsync();

            return Ok(new CosProdusDTO(newcomanda));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCosProdus(Cosprodus x)
        {

            _repository.Update(x);
            await _repository.SaveAsync();
            return Ok(new CosProdusDTO(x));
        }

    }
}
