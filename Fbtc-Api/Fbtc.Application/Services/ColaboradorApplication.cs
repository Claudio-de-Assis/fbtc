﻿using System;
using System.Collections.Generic;

using Fbtc.Domain.Entities;
using Fbtc.Domain.Interfaces.Services;
using Fbtc.Application.Interfaces;

using prmToolkit.Validation;
using Fbtc.Application.Helper;

namespace Fbtc.Application.Services
{
    public class ColaboradorApplication : IColaboradorApplication
    {
        private readonly IColaboradorService _colaboradorService;

        public ColaboradorApplication(IColaboradorService colaboradorService)
        {
            _colaboradorService = colaboradorService;
        }

        public string DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Colaborador> FindByFilters(string nome, int perfilId, bool? ativo)
        {
            string _nome, _tipoPerfil;
            
            _nome = nome == "0" ? "" : nome;
            // _tipoPerfil = tipoPerfil == "0" ? "" : tipoPerfil;

            return _colaboradorService.FindByFilters(_nome, perfilId, ativo);
        }

        public IEnumerable<Colaborador> GetAll()
        {
            return _colaboradorService.GetAll();
        }

        public Colaborador GetColaboradorById(int id)
        {
            return _colaboradorService.GetColaboradorById(id);
        }

        public string RessetPasswordById(int id)
        {
            return _colaboradorService.RessetPasswordById(id);
        }

        public string Save(Colaborador c)
        {
            ArgumentsValidator.RaiseExceptionOfInvalidArguments(
                RaiseException.IfNullOrEmpty(c.Nome, "Nome do Colaborador não informado"),
                RaiseException.IfNotEmail(c.EMail, "E-Mail inválido"),
                RaiseException.IfNullOrEmpty(c.NrCelular, "NrCelular não informado"),
                RaiseException.IfEqualsZero(c.PerfilId, "Perfil não informado")
            );

            Colaborador _c = new Colaborador()
            {
                ColaboradorId = c.ColaboradorId,
                PessoaId = c.PessoaId,
                Nome = Functions.AjustaTamanhoString(c.Nome, 100),
                EMail = Functions.AjustaTamanhoString(c.EMail, 100),
                NomeFoto = Functions.AjustaTamanhoString(c.NomeFoto, 100),
                Sexo = c.Sexo,
                DtNascimento = c.DtNascimento,
                NrCelular = Functions.AjustaTamanhoString(c.NrCelular, 15),
                PasswordHash = Functions.AjustaTamanhoString(c.PasswordHash, 100),
                PerfilId = c.PerfilId,
                Ativo = c.Ativo
            };

            try
            {
                if (_c.PessoaId == 0)
                {
                    return _colaboradorService.Insert(_c);
                }
                else
                {
                    return _colaboradorService.Update(c.PessoaId, _c);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Colaborador SetColaborador()
        {
            Colaborador c = new Colaborador() {
                ColaboradorId = 0,
                PessoaId = 0,
                Nome = "",
                EMail = "",
                NomeFoto = "",
                Sexo = "",
                DtNascimento = null,
                NrCelular = "",
                PasswordHash = "",
                Ativo = true,
                DtCadastro = null
            };

            return c;
        }

        public string ValidaEMail(int colaboradorId, string eMail)
        {
            return _colaboradorService.ValidaEMail(colaboradorId, eMail);
        }
    }
}
