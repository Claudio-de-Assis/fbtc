using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Repositories;
using Fbtc.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Fbtc.Domain.Services
{
    public class AssinaturaEventoService : IAssinaturaEventoService
    {
        private readonly IAssinaturaEventoRepository _assinaturaEventoRepository;

        public AssinaturaEventoService(IAssinaturaEventoRepository assinaturaEventoRepository)
        {
            _assinaturaEventoRepository = assinaturaEventoRepository;
        }

        public IEnumerable<AssinaturaEventoDao> FindByFilters(int eventoId, bool? ativo)
        {
            return _assinaturaEventoRepository.FindByFilters(eventoId, ativo);
        }

        public IEnumerable<AssinaturaEvento> GetAll()
        {
            return _assinaturaEventoRepository.GetAll();
        }

        public AssinaturaEventoDao GetAssinaturaEventoById(int id)
        {
            return _assinaturaEventoRepository.GetAssinaturaEventoById(id);
        }

        public string Insert(AssinaturaEvento assinaturaEvento)
        {
            return _assinaturaEventoRepository.Insert(assinaturaEvento);
        }

        public string Update(int id, AssinaturaEvento assinaturaEvento)
        {
            return _assinaturaEventoRepository.Update(id, assinaturaEvento);
        }
    }
}
