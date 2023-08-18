package com.ecoekb.ui.navigation

import androidx.annotation.StringRes
import androidx.compose.ui.graphics.vector.ImageVector

sealed class Screens(
    val route: String,
    val prefixRoute: String,
    @StringRes val resourceId: Int?,
    val image: ImageVector?
) {
    //    Screen start
    object Login : Screens("Login", "Login", null, null)
    object Registration : Screens("Registration", "Registration", null, null)
    object OnBoarding : Screens("OnBoarding", "OnBoarding", null, null)

    object RequestsItem : Screens("RequestsItem/{requestId}", "RequestsItem/", null, null)
    object RequestsList : Screens("RequestsList", "RequestsList", null, null)
    object RequestsCreate : Screens("RequestsCreate", "RequestsCreate", null, null)
}