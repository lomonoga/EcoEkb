package com.ecoekb.ui.screens.authScreens.auth

import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxHeight
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material3.Button
import androidx.compose.material3.CircularProgressIndicator
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.material3.TextButton
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.remember
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.navigation.NavController
import com.ecoekb.ui.composeble.InputField
import com.ecoekb.ui.navigation.Screens

@Composable
fun AuthScreen(
    navController: NavController,
    viewModel: AuthViewModel = hiltViewModel()
) {
    val email = remember { viewModel.email }
    val password = remember { viewModel.password }

    val correctEmail = remember { viewModel.correctEmail }
    val correctPassword = remember { viewModel.correctPassword }

    val loginCorrect = remember { viewModel.loginCorrect }

    Box(
        modifier = Modifier.background(MaterialTheme.colorScheme.background)
    ) {
        Column {

            Column(
                verticalArrangement = Arrangement.Top,
                modifier = Modifier
                    .padding(
                        top = 16.dp,
                        start = 16.dp,
                        end = 16.dp,
                    )
            ) {

                Text(
                    text = "Вход",
                    style = MaterialTheme.typography.titleMedium,
                )

                if (viewModel.status.value == Status.ERROR) {
                    Text(
                        text = "Неправильный логин или пароль",
                        style = MaterialTheme.typography.bodyMedium,
                        color = MaterialTheme.colorScheme.error,
                        modifier = Modifier
                            .padding(
                                top = 16.dp
                            )
                    )
                }

                if (viewModel.status.value == Status.WAIT){
                    CircularProgressIndicator()
                } else {
                    InputField(
                        value = email,
                        placeholder = "Email",
                        isError = correctEmail,
                        keyboardOptions = KeyboardOptions(keyboardType = KeyboardType.Email)
                    )
                    InputField(
                        value = password,
                        placeholder = "Password",
                        isError = correctPassword,
                        keyboardOptions = KeyboardOptions(keyboardType = KeyboardType.Password)
                    )

                    TextButton(
                        onClick = { navController.navigate(Screens.Registration.route) },
                    ) {
                        Text("Зарегестрироваться")
                    }
                }

                Column(
                    modifier = Modifier.fillMaxHeight(),
                    verticalArrangement = Arrangement.Bottom
                ) {
                    Button(
                        onClick = {
                            if (viewModel.checkCorrectCredits()) {
                                viewModel.loginUserByCredits()
                            } },
                        modifier = Modifier
                            .padding(16.dp)
                            .fillMaxWidth()
                            .height(60.dp),
                        shape = RoundedCornerShape(5.dp)
                    ) {
                        Text("Войти")
                    }
                }

            }
        }
    }
    if (loginCorrect.value){
        LaunchedEffect(true) {
            navController.navigate(Screens.RequestsList.route)
        }
    }
}

