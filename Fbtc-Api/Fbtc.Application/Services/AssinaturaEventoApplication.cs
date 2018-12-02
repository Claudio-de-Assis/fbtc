using Fbtc.Application.Helper;
using Fbtc.Application.Interfaces;
using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Services;
using prmToolkit.Validation;
using System;
using System.Collections.Generic;

namespace Fbtc.Application.Services
{
    public class AssinaturaEventoApplication : IAssinaturaEventoApplication
    {
        private readonly IAssinaturaEventoService _assinaturaEventoService;

        public AssinaturaEventoApplication(IAssinaturaEventoService assinaturaEventoService)
        {
            _assinaturaEventoService = assinaturaEventoService;
        }

        public IEnumerable<AssinaturaEventoDao> FindByFilters(int eventoId, bool? ativo)
        {
            return _assinaturaEventoService.FindByFilters(eventoId, ativo);
        }

        public IEnumerable<AssinaturaEvento> GetAll()
        {
            return _assinaturaEventoService.GetAll();
        }

        public AssinaturaEventoDao GetAssinaturaEventoById(int id)
        {
            return _assinaturaEventoService.GetAssinaturaEventoById(id);
        }

        public string Save(AssinaturaEventoDao a)
        {
            AssinaturaEvento assinaturaEvento = new AssinaturaEvento
            {
                AssinaturaEventoId = a.AssinaturaEventoId,
                AssociadoId = a.AssociadoId,
                ValorEventoPublicoId = a.ValorEventoPublicoId,
                PercentualDesconto = a.PercentualDesconto,
                TipoDesconto = a.TipoDesconto,
                DtAssinatura = a.DtAssinatura,
                DtAtualizacao = a.DtAtualizacao,
                Ativo = a.Ativo
            };

            try
            {
                if (assinaturaEvento.AssinaturaEventoId == 0)
                {
                    return _assinaturaEventoService.Insert(assinaturaEvento);
                }
                else
                {
                    return _assinaturaEventoService.Update(assinaturaEvento.AssinaturaEventoId, assinaturaEvento);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
