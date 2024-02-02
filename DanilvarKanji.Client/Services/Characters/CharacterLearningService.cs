using Blazored.Modal;
using Blazored.Modal.Services;
using DanilvarKanji.Client.Shared.Modal;
using Microsoft.AspNetCore.Components;

namespace DanilvarKanji.Client.Services.Characters;

public class CharacterLearningService
{
    private readonly IModalService _modalService;
    private readonly ICharacterLearningHttpService _learningHttpService;
    private readonly NavigationManager _navigationManager;

    public CharacterLearningService(ICharacterLearningHttpService learningHttpService,
        NavigationManager navigationManager, IModalService modalService)
    {
        _learningHttpService = learningHttpService;
        _navigationManager = navigationManager;
        _modalService = modalService;
    }

    public void SkipCharacterFromLearning(string learningIdToSkip, string naviteToAfterSkip = "")
    {
        try
        {
            var options = new ModalOptions
            {
                Position = ModalPosition.Middle,
                Class = "custom-modal"
            };

            var parameters = new ModalParameters()
                .Add(nameof(ConfirmCancelModal.Message), "AGH")
                .Add(nameof(ConfirmCancelModal.OnConfirm), () =>
                {
                    _learningHttpService.ToggleSkipStateAsync(learningIdToSkip);

                    if (!string.IsNullOrEmpty(naviteToAfterSkip))
                        _navigationManager.NavigateTo(naviteToAfterSkip);
                });

            _modalService.Show<ConfirmCancelModal>("AHAHHAH", parameters, options);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}