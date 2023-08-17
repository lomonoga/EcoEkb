package com.ecoekb.ui.screens.authScreens.registration

import android.widget.Toast
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxHeight
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.KeyboardArrowLeft
import androidx.compose.material3.Button
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.material3.TextButton
import androidx.compose.runtime.Composable
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalContext
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.navigation.NavController
import com.ecoekb.ui.composeble.InputField

@Composable
fun RegistrationScreen(
    navController: NavController,
    viewModel: RegistrationViewModel = hiltViewModel()
) {
    val part = remember { mutableStateOf(1) }

    val firstName = remember { viewModel.firstName }
    val secondName = remember { viewModel.secondName }
    val surnameText = remember { viewModel.surName }

    val correctFirstName = remember { viewModel.correctFirstName }
    val correctSecondName = remember { viewModel.correctSecondName }


    val email = remember { viewModel.email }
    val password = remember { viewModel.password }
    val passwordDuplicate = remember { viewModel.passwordDuplicate }

    val correctEmail = remember { viewModel.correctEmail }
    val correctPassword = remember { viewModel.correctPassword }
    val correctPasswordDuplicate = remember { viewModel.correctPasswordDuplicate }


    Box (
        modifier = Modifier.background(MaterialTheme.colorScheme.background)
    ) {
        Column {
            Row(
                verticalAlignment = Alignment.CenterVertically,
                modifier = Modifier.padding(16.dp)
            ) {

                var color = MaterialTheme.colorScheme.secondary
                var enabled = true
                if (part.value == 1) {
                    color = MaterialTheme.colorScheme.background
                    enabled = false
                }

                TextButton(
                    onClick = { part.value = 1 },
                    enabled = enabled
                ){
                    Icon(
                        imageVector = Icons.Default.KeyboardArrowLeft,
                        tint = color,
                        contentDescription = null
                    )
                    Text(
                        text = "Предыдущий шаг",
                        color = color,
                        style = MaterialTheme.typography.bodyMedium
                    )
                }
            }


            Column(
                verticalArrangement = Arrangement.Top,
                modifier = Modifier
                    .padding(
                        start = 16.dp,
                        end = 16.dp,
                        )
            ) {
                Text(
                    text = "Регистрация",
                    style = MaterialTheme.typography.titleMedium,
                )

                Row(
                    modifier = Modifier.padding(
                        top = 16.dp
                    )
                ) {
                    Text(
                        text = "*",
                        style = MaterialTheme.typography.bodyMedium,
                    )

                    Text(
                        text = "Выделенные поля являются обязательными для заполнения",
                        style = MaterialTheme.typography.bodyMedium,
                        modifier = Modifier.padding(start = 5.dp)
                    )
                }

                if (part.value == 1) {

                    InputField(
                        value = firstName,
                        placeholder = "Имя *",
                        isError = correctFirstName
                    )
                    InputField(
                        value = secondName,
                        placeholder = "Фамилия *",
                        isError = correctSecondName
                    )
                    InputField(
                        value = surnameText,
                        placeholder = "Отчество"
                    )
                } else if (part.value == 2) {

                    InputField(
                        value = email,
                        placeholder = "Электронная почта *",
                        isError = correctEmail
                    )
                    InputField(
                        value = password,
                        placeholder = "Пароль *",
                        isError = correctPassword
                    )
                    InputField(
                        value = passwordDuplicate,
                        placeholder = "Повторите пароль",
                        isError = correctPasswordDuplicate
                    )
                } else if (part.value == 3) {
                    Toast.makeText(LocalContext.current, "This is a Sample Toast", Toast.LENGTH_LONG).show()
                    part.value = 2
                }
            }

            Column (
                modifier = Modifier.fillMaxHeight(),
                verticalArrangement = Arrangement.Bottom
            ) {
                Button(
                    onClick = {

                        if (part.value == 1) {
                            if (viewModel.checkCorrectFullName()) {
                                part.value = 2
                            }
                        } else if (part.value == 2) {
                            if (viewModel.checkCorrectPassword()) {
                                if (!viewModel.checkEqualsPassword()) {
                                    part.value = 3
                                }
                            }
                        }

                    },
                    modifier = Modifier
                        .padding(16.dp)
                        .fillMaxWidth()
                        .height(60.dp),
                    shape = RoundedCornerShape(5.dp)
                ){
                    Text("Продолжить")
                }
            }

        }
    }
}
