﻿using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EnderecoController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    public EnderecoController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdcionaEndereco([FromBody] CreateEnderecoDto 
        enderecoDto)
    {
        Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
        _context.Enderecos.Add(endereco);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaEnderecosPorId), new
        { Id = endereco.Id }, endereco);
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaEnderecosPorId(int id)
    {
        Endereco endereco = _context.Enderecos.FirstOrDefault(endereco =>
        endereco.Id == id);
        if (endereco != null)
        {
            ReadEnderecoDto enderecoDto = _mapper.Map<ReadEnderecoDto>
                (endereco);

            return Ok(enderecoDto);
        }
        return NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaEndereco(int id, [FromBody] UpdateEnderecoDto
        enderecoDto)
    {
        Endereco endereco = _context.Enderecos.FirstOrDefault(endereco =>
        endereco.Id == id);
        if (endereco == null)
        {
            return NotFound();
        }
        _mapper.Map(enderecoDto, endereco);
        _context.SaveChanges();
        return NoContent();
    }

}
