using System.Net.Mime;
using EcoNotifications.Backend.QRCode.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EcoNotifications.Backend.QRCode.Controllers;

[ApiController]
[Route("[controller]")]
public class QrCodeController : ControllerBase
{
    /// <summary>
    ///     Генерирует и отдаёт qr-код с ссылкой на подачу заявления
    /// </summary>
    /// <param name="generateQrRequest">Место и ответсвтенная компания</param>
    /// <returns>PNG файл с QR-кодом</returns>
    /// <exception cref="Exception">Произошла ошибка</exception>
    [HttpPost("generate-qr")]
    public async Task<IActionResult> GenerateQrCode([FromBody]GenerateQrRequest generateQrRequest)
    {
        //Заменить на нудный IP
        var urlService = "localhost";
        
        var qrCodeDat = $"{ urlService }?address={ generateQrRequest.Address }?" +
                        $"company={ generateQrRequest.Company }?topic=GarbageAndDirty";
        var urlQR = $"http://api.qrserver.com/v1/create-qr-code/?data={ qrCodeDat }!&size=450x450";
        
        var response = await new HttpClient().GetAsync(urlQR);

        if (response.IsSuccessStatusCode)
            return File(await response.Content.ReadAsStreamAsync(), "image/png", "qrcode.png");
        
        throw new Exception($"Failed to retrieve image from API. Status code: {response.StatusCode}");
    }
}