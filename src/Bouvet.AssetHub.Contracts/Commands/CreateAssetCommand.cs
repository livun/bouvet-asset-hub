﻿using Bouvet.AssetHub.Contracts.Dtos;
using LanguageExt;
using MediatR;

namespace Bouvet.AssetHub.Contracts.Commands
{
    public record CreateAssetCommand(int SerialNumberValue, int CategoryId) : IRequest<Option<AssetResponseDto>>;

}