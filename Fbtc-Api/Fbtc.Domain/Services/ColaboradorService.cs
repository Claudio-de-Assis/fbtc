﻿using System;
using System.Collections.Generic;

using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;
using Fbtc.Domain.Interfaces.Services;

namespace Fbtc.Domain.Services
{
    public class ColaboradorService : IColaboradorService
    {
        private readonly IColaboradorRepository _colaboradorRepository;

        public ColaboradorService(IColaboradorRepository colaboradorRepository)
        {
            _colaboradorRepository = colaboradorRepository;
        }

        public string DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Colaborador> FindByFilters(string nome, int perfilId, bool? ativo)
        {
            return _colaboradorRepository.FindByFilters(nome, perfilId, ativo);
        }

        public IEnumerable<Colaborador> GetAll()
        {
            return _colaboradorRepository.GetAll();
        }

        public Colaborador GetColaboradorById(int id)
        {
            return _colaboradorRepository.GetColaboradorById(id);
        }

        public string Insert(Colaborador colaborador)
        {
            return _colaboradorRepository.Insert(colaborador);
        }

        public string RessetPasswordById(int id)
        {
            return _colaboradorRepository.RessetPasswordById(id);
        }

        public string Update(int id, Colaborador colaborador)
        {
            return _colaboradorRepository.Update(id, colaborador);
        }

        public string ValidaEMail(int colaboradorId, string eMail)
        {
            return _colaboradorRepository.ValidaEMail(colaboradorId, eMail);
        }
    }
}
