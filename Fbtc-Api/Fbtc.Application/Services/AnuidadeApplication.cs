using System;
using System.Collections.Generic;

using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Services;
using Fbtc.Application.Interfaces;

using prmToolkit.Validation;
using Fbtc.Application.Helper;

namespace Fbtc.Application.Services
{
    public class AnuidadeApplication : IAnuidadeApplication
    {
        private readonly IAnuidadeService _anuidadeService;

        public AnuidadeApplication(IAnuidadeService anuidadeService)
        {
            _anuidadeService = anuidadeService;
        }
        
        public string DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Anuidade> FindByFilters(int exercicio, bool? ativo)
        {
            return _anuidadeService.FindByFilters(exercicio, ativo);
        }

        public IEnumerable<Anuidade> GetAll()
        {
            return _anuidadeService.GetAll();
        }

        public Anuidade GetAnuidadeById(int id)
        {
            return _anuidadeService.GetAnuidadeById(id);
        }

        public AnuidadeDao GetAnuidadeDaoById(int id)
        {
            return _anuidadeService.GetAnuidadeDaoById(id);
        }

        public AnuidadeDao GetAnuidadeDaoByIdTipoPublicoId(int id, int tipoPublicoId)
        {
            return _anuidadeService.GetAnuidadeDaoByIdTipoPublicoId(id, tipoPublicoId);
        }

        public IEnumerable<Anuidade> GetAnuidadesPendentesByPessoaId(int pessoaId)
        {
            return _anuidadeService.GetAnuidadesPendentesByPessoaId(pessoaId);
        }

        public IEnumerable<AnuidadeTipoPublicoDao> GetAnuidadeTipoPublicoDaoByAnuidadeId(int id)
        {
            return _anuidadeService.GetAnuidadeTipoPublicoDaoByAnuidadeId(id);
        }

        public IEnumerable<TipoPublico> GetTiposPublicosToAnuidade()
        {
            return _anuidadeService.GetTiposPublicosToAnuidade();
        }

        public IEnumerable<ValorAnuidade> GetValoresAnuidadesByAnuidadeTipoPublicoId(int id)
        {
            return _anuidadeService.GetValoresAnuidadesByAnuidadeTipoPublicoId(id);
        }

        public string Save(Anuidade a)
        {
            ArgumentsValidator.RaiseExceptionOfInvalidArguments(
            RaiseException.IfTrue(a.Exercicio == 0, "Exercício não informado")
            );

            Anuidade _a = new Anuidade {
                AnuidadeId = a.AnuidadeId,
                Exercicio = a.Exercicio,
                DtVencimento = a.DtVencimento,
                DtInicioVigencia = a.DtInicioVigencia,
                DtTerminoVigencia = a.DtTerminoVigencia,
                CobrancaLiberada = a.CobrancaLiberada,
                DtCobrancaLiberada = a.DtCobrancaLiberada,
                DtCadastro = a.DtCadastro,
                Ativo = a.Ativo
            };


            try
            {
                if (_a.AnuidadeId == 0)
                {
                    return _anuidadeService.Insert(_a);
                }
                else
                {
                    return _anuidadeService.Update(a.AnuidadeId, _a);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SaveAnuidadeDao(AnuidadeDao a)
        {
            IEnumerable<TipoPublico> tiposPublicos = GetTiposPublicosToAnuidade();

            List<TipoPublico> lstTipoPublico = new List<TipoPublico>();

            foreach (var tp in tiposPublicos)
            {
                lstTipoPublico.Add(tp);
            }

            ArgumentsValidator.RaiseExceptionOfInvalidArguments(
                RaiseException.IfTrue(a.Exercicio == 0, "Exercício não informado"),
                RaiseException.IfFalse(Functions.CheckDate(a.DtVencimento.ToString()),"Data inválida"),
                RaiseException.IfFalse(Functions.CheckDate(a.DtInicioVigencia.ToString()), "Data início vigência inválida"),
                RaiseException.IfFalse(Functions.CheckDate(a.DtTerminoVigencia.ToString()), "Data término vigência inválida"),
                RaiseException.IfTrue(a.AnuidadesTiposPublicosDao == null, "Anuidade tipo público está nulo")
            );

            foreach (var atp in a.AnuidadesTiposPublicosDao)
            {
                ArgumentsValidator.RaiseExceptionOfInvalidArguments(
                    RaiseException.IfTrue(atp.ValoresAnuidades == null, "Valores anuidades está nulo")
                );

                atp.TipoPublicoId = lstTipoPublico.Find(x => x.Codigo == atp.Codigo).TipoPublicoId;

                ArgumentsValidator.RaiseExceptionOfInvalidArguments(
                    RaiseException.IfTrue(atp.TipoPublicoId == 0, "Tipo Publico não encontrado")
                );
            }

            // Setando a primeira data de inicio da cobrança:
            if (a.CobrancaLiberada == true && a.DtCobrancaLiberada == null)
                a.DtCobrancaLiberada = DateTime.Now;
            
            AnuidadeDao _a = new AnuidadeDao
            {
                AnuidadeId = a.AnuidadeId,
                Exercicio = a.Exercicio,
                DtVencimento = a.DtVencimento,
                DtInicioVigencia = a.DtInicioVigencia,
                DtTerminoVigencia = a.DtTerminoVigencia,
                CobrancaLiberada = a.CobrancaLiberada,
                DtCobrancaLiberada = a.DtCobrancaLiberada,
                DtCadastro = a.DtCadastro,
                Ativo = a.Ativo,
                AnuidadesTiposPublicosDao = a.AnuidadesTiposPublicosDao
            };

            try
            {
                if (_a.AnuidadeId == 0)
                {
                    return _anuidadeService.InsertAnuidadeDao(_a);
                }
                else
                {
                    return _anuidadeService.UpdateAnuidadeDao(a.AnuidadeId, _a);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Anuidade SetAnuidade()
        {
            Anuidade _a = new Anuidade
            {
                AnuidadeId = 0,
                Exercicio = 0,
                DtVencimento = DateTime.Now,
                DtInicioVigencia = DateTime.Now,
                DtTerminoVigencia = DateTime.Now,
                CobrancaLiberada = false,
                DtCobrancaLiberada = null,
                DtCadastro = DateTime.Now,
                Ativo = false
            };

            return _a;
        }
    }
}
