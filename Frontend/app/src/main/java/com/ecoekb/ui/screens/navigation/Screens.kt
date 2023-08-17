package com.ecoekb.ui.screens.navigation

import androidx.annotation.StringRes
import androidx.compose.ui.graphics.vector.ImageVector

sealed class Screens(
    val route: String,
    @StringRes val resourceId: Int?,
    val image: ImageVector?
) {
    //    Screen start
    object Login : Screens("Login", null, null)
    object Registration : Screens("Registration", null, null)
    object OnBoarding : Screens("OnBoarding", null, null)
}