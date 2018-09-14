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

        public IEnumerable<Anuidade> FindByFilters(int codigo, bool? ativo)
        {
            return _anuidadeService.FindByFilters(codigo, ativo);
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

        public string Save(Anuidade a)
        {
            ArgumentsValidator.RaiseExceptionOfInvalidArguments(
            RaiseException.IfTrue(a.Codigo == 0, "Código não informado")
            );

            Anuidade _a = new Anuidade { AnuidadeId = a.AnuidadeId, Codigo = a.Codigo, DtCadastro = a.DtCadastro, Ativo = a.Ativo };


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
            ArgumentsValidator.RaiseExceptionOfInvalidArguments(
            RaiseException.IfTrue(a.Codigo == 0, "Código não informado")
            );

            AnuidadeDao _a = new AnuidadeDao
            {
                AnuidadeId = a.AnuidadeId,
                Codigo = a.Codigo,
                DtCadastro = a.DtCadastro,
                Ativo = a.Ativo,
                TiposPublicosValorsAnuidadesDao = a.TiposPublicosValorsAnuidadesDao
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
            Anuidade _a = new Anuidade { AnuidadeId = 0, Codigo = 0, DtCadastro = DateTime.Now, Ativo = true };

            return _a;
        }
    }
}
