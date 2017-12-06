using System;
using System.Collections.Generic;

using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Services;
using Fbtc.Application.Interfaces;

using prmToolkit.Validation;
using Fbtc.Application.Helper;

namespace Fbtc.Application.Services
{
    public class AtcApplication : IAtcApplication
    {
        private readonly IAtcService _atcService;

        public AtcApplication(IAtcService atcService)
        {
            _atcService = atcService;
        }

        public string DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Atc> GetAll()
        {
            return _atcService.GetAll();
        }

        public Atc GetAtcById(int id)
        {
            return _atcService.GetAtcById(id);
        }

        public string Save(Atc a)
        {
            ArgumentsValidator.RaiseExceptionOfInvalidArguments(
                RaiseException.IfNullOrEmpty(a.Nome, "Nome da ATC não informado"),
                RaiseException.IfNullOrEmpty(a.NomePres, "Nome do Presidente não informado")
            );

            Atc _a = new Atc() {
                AtcId = a.AtcId,
                Nome = Functions.AjustaTamanhoString(a.Nome, 100),
                UF = Functions.AjustaTamanhoString(a.UF, 2),
                NomePres = Functions.AjustaTamanhoString(a.NomePres, 100),
                NomeVPres = Functions.AjustaTamanhoString(a.NomeVPres, 100),
                NomePSec = Functions.AjustaTamanhoString(a.NomePSec, 100),
                NomeSSec = Functions.AjustaTamanhoString(a.NomeSSec, 100),
                NomePTes = Functions.AjustaTamanhoString(a.NomePTes, 100),
                NomeSTes = Functions.AjustaTamanhoString(a.NomeSTes, 100),
                Site = Functions.AjustaTamanhoString(a.Site, 100),
                SiteDiretoria = Functions.AjustaTamanhoString(a.SiteDiretoria, 100),
                Ativo = a.Ativo,
                Codigo = a.Codigo
            };

            try
            {
                if (_a.AtcId == 0)
                {
                    return _atcService.Insert(_a);
                }
                else
                {
                    return _atcService.Update(a.AtcId, _a);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Atc SetAtc()
        {
            Atc _a = new Atc() { AtcId = 0, Nome = "", UF = "", Ativo = true, Codigo = 0 };

            return _a;
        }
    }
}
