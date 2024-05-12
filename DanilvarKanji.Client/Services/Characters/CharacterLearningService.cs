using Blazored.Modal;
using Blazored.Modal.Services;
using DanilvarKanji.Client.Shared.Modal;
using Microsoft.AspNetCore.Components;

namespace DanilvarKanji.Client.Services.Characters;

public class CharacterLearningService
{
  private readonly ICharacterLearningApiService _learningApiService;

  private readonly IModalService _modalService;
  private readonly NavigationManager _navigationManager;

  public CharacterLearningService(
    ICharacterLearningApiService learningApiService,
    NavigationManager navigationManager,
    IModalService modalService
  )
  {
    _learningApiService = learningApiService;
    _navigationManager = navigationManager;
    _modalService = modalService;
  }

  public bool ToggledSkipState { get; set; }

  public void ToggleCharLearningSkipState(
    string learningIdToSkip,
    string naviteToAfterSkip = "",
    bool forceReload = false
  )
  {
    try
    {
      var options = new ModalOptions { Position = ModalPosition.Middle, Class = "custom-modal" };

      ModalParameters parameters = new ModalParameters()
        .Add(nameof(ConfirmCancelModal.Message), "AGH")
        .Add(
          nameof(ConfirmCancelModal.OnConfirm),
          () =>
          {
            _learningApiService.ToggleSkipStateAsync(learningIdToSkip);
            ToggledSkipState = true;

            if (!string.IsNullOrEmpty(naviteToAfterSkip))
              _navigationManager.NavigateTo(naviteToAfterSkip, forceReload);
          }
        );

      _modalService.Show<ConfirmCancelModal>("AHAHHAH", parameters, options);
    }
    catch (HttpRequestException e)
    {
      Console.WriteLine(e);
      throw;
    }
  }
}