using System;
using System.Collections.Generic;

using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Services;
using Fbtc.Application.Interfaces;

using prmToolkit.Validation;
using Fbtc.Application.Helper;

namespace Fbtc.Application.Services
{
    public class IsencaoApplication : IIsencaoApplication
    {
        private readonly IIsencaoService _isencaoService;

        public IsencaoApplication(IIsencaoService isencaoService)
        {
            _isencaoService = isencaoService;
        }

        public string DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Isencao> FindByFilters(string tipoIsencao, string nomeAssociado, string descricao, 
            int ano, int eventoId)
        {
            string _nomeAssociado, _descricao;

            _nomeAssociado = nomeAssociado == "0" ? "" : nomeAssociado;
            _descricao = descricao == "0" ? "" : descricao;

            return _isencaoService.FindByFilters(tipoIsencao, _nomeAssociado, _descricao, ano, eventoId);
        }

        public IEnumerable<IsencaoDao> FindIsencaoByFilters(string tipoIsencao, string nomeAssociado, int ano, string identificacao, string tipoEvento)
        {
            string _nomeAssociado, _identificacao;

            _nomeAssociado = nomeAssociado == "0" ? "" : nomeAssociado;
            _identificacao = identificacao == "0" ? "" : identificacao;

            return _isencaoService.FindIsencaoByFilters(tipoIsencao, _nomeAssociado, ano, _identificacao, tipoEvento);
        }

        public IEnumerable<Isencao> GetAll(string tipoIsencao)
        {
            return _isencaoService.GetAll(tipoIsencao);
        }

        public Isencao GetIsencaoById(int id)
        {
            return _isencaoService.GetIsencaoById(id);
        }

        public string Save(Isencao i)
        {
            ArgumentsValidator.RaiseExceptionOfInvalidArguments(
                RaiseException.IfNullOrEmpty(i.Descricao, "Descrição não informada"),
                RaiseException.IfTrue(i.DtAta == DateTime.MinValue, "Data não informada"),
                RaiseException.IfNull(i.DtAta, "Data não informada - Nula"),
                RaiseException.IfTrue(i.AnoEvento == 0, "Ano da Isencão não informada")
            );

            if (i.TipoIsencao.Equals("1"))
            {
                ArgumentsValidator.RaiseExceptionOfInvalidArguments(
                    RaiseException.IfTrue(i.EventoId == 0, "Evento não informado"),
                    RaiseException.IfNull(i.EventoId, "Evento não informado")
                    );
            }

            if (i.TipoIsencao.Equals("2"))
            {
                ArgumentsValidator.RaiseExceptionOfInvalidArguments(
                    RaiseException.IfTrue(i.AnuidadeId == 0, "Anuidade não informada"),
                    RaiseException.IfNull(i.AnuidadeId, "Anuidade não informada")
                    );
            }


            Isencao _i = new Isencao {
                IsencaoId = i.IsencaoId,
                AnuidadeId = i.AnuidadeId,
                EventoId = i.EventoId,
                Descricao = i.Descricao,
                DtAta = i.DtAta,
                AnoEvento = i.AnoEvento,
                TipoIsencao = i.TipoIsencao,
                Ativo = i.Ativo
            };

            try
            {
                if (_i.IsencaoId == 0)
                {
                    return _isencaoService.Insert(_i);
                }
                else
                {
                    return _isencaoService.Update(i.IsencaoId, _i);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Isencao SetIsencao(string tipoIsencao)
        {
            Isencao _i = new Isencao
            {
                IsencaoId = 0,
                AnuidadeId = null,
                EventoId = null,
                Descricao = "",
                DtAta = null,
                AnoEvento = 0,
                TipoIsencao = tipoIsencao,
                Ativo = true
            };
            return _i;
        }
    }
}
