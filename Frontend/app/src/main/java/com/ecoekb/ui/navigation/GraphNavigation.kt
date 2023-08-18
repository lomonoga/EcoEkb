package com.ecoekb.ui.navigation

import android.annotation.SuppressLint
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.Scaffold
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalContext
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import com.ecoekb.ui.screens.authScreens.auth.AuthScreen
import com.ecoekb.ui.screens.authScreens.registration.RegistrationScreen
import com.ecoekb.ui.screens.onboarding.OnBoarding
import com.ecoekb.ui.screens.onboarding.OnboardingDataStore
import com.ecoekb.ui.screens.requestsScreens.requestItem.RequestsItem
import com.ecoekb.ui.screens.requestsScreens.requestsList.RequestsList
import com.ecoekb.ui.screens.requestsScreens.requestsCreate.RequestsCreate
import kotlinx.coroutines.DelicateCoroutinesApi
import kotlinx.coroutines.flow.first
import kotlinx.coroutines.runBlocking

@SuppressLint("CoroutineCreationDuringComposition")
@OptIn(ExperimentalMaterial3Api::class, DelicateCoroutinesApi::class)
@Composable
fun GraphNavigation(){
    val navController = rememberNavController()

    val onboardingDataStore = OnboardingDataStore(LocalContext.current)
    var startScreen = Screens.Login.route

    runBlocking {
        val i = onboardingDataStore.getOnboardingShow().first()
        if (onboardingDataStore.getOnboardingShow().first()){
            startScreen = Screens.OnBoarding.route
        }
    }

    Scaffold { innerPadding ->
        NavHost(navController, startDestination = startScreen, Modifier.padding(innerPadding)) {
            composable(Screens.Login.route) { AuthScreen(navController) }
            composable(Screens.Registration.route) { RegistrationScreen(navController) }
            composable(Screens.OnBoarding.route) { OnBoarding(navController) }

            composable(Screens.RequestsItem.route) { backStackEntry ->
                val requestId: String = backStackEntry.arguments?.getString("requestId")!!
                RequestsItem(requestId)
            }
            composable(Screens.RequestsList.route) { RequestsList(navController) }
            composable(Screens.RequestsCreate.route) { RequestsCreate(navController) }
        }
    }
}