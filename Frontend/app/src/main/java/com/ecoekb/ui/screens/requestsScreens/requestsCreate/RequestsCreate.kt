package com.ecoekb.ui.screens.requestsScreens.requestsCreate

import androidx.compose.foundation.background
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.defaultMinSize
import androidx.compose.foundation.layout.fillMaxHeight
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.KeyboardArrowDown
import androidx.compose.material.icons.filled.KeyboardArrowUp
import androidx.compose.material3.Button
import androidx.compose.material3.DropdownMenu
import androidx.compose.material3.DropdownMenuItem
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedTextField
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.MutableState
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.clip
import androidx.compose.ui.geometry.Size
import androidx.compose.ui.layout.onGloballyPositioned
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.toSize
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.navigation.NavController
import com.ecoekb.ui.composeble.InputField
import com.ecoekb.ui.navigation.Screens

@Composable
fun RequestsCreate(
    navController: NavController,
    viewModel: RequestsCreateViewModel = hiltViewModel()
) {
    val title = remember { viewModel.title }
    val content = remember { viewModel.content }
    val address = remember { viewModel.address }


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
                    text = "Добавить обращение",
                    style = MaterialTheme.typography.titleMedium,
                )

                MyContent(title)

                InputField(
                    value = content,
                    placeholder = "Кратко опишите проблему...",
                    modifier = Modifier.defaultMinSize(minHeight = 120.dp)
                )

                InputField(
                    value = address,
                    placeholder = "Введите адрес"
                )


                Column(
                    modifier = Modifier.fillMaxHeight(),
                    verticalArrangement = Arrangement.Bottom
                ) {
                    Button(
                        onClick = {
                            viewModel.createRequest()
                            navController.navigate(Screens.RequestsList.route)
                                  },
                        modifier = Modifier
                            .fillMaxWidth()
                            .height(60.dp),
                        shape = RoundedCornerShape(5.dp)
                    ) {
                        Text("Добавить обращение")
                    }
                }

            }
        }
    }
}


@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun MyContent(
    title: MutableState<String>
){

    var mExpanded = remember { mutableStateOf(false) }

    val mCities = listOf(
        "Грязь и мусор на улице",
        "Ликвидация свалки",
        "Нелегальная торговля",
        "Другое..."
    )

    var mTextFieldSize = remember { mutableStateOf(Size.Zero)}

    val icon = if (mExpanded.value)
        Icons.Filled.KeyboardArrowUp
    else
        Icons.Filled.KeyboardArrowDown

    Column(
        modifier = Modifier.padding(top = 15.dp)
    ) {

        // Create an Outlined Text Field
        // with icon and not expanded
        OutlinedTextField(
            value = title.value,
            onValueChange = { title.value = it },
            modifier = Modifier
                .fillMaxWidth()
                .onGloballyPositioned { coordinates ->
                    mTextFieldSize.value = coordinates.size.toSize()
                },
            label = {Text("Название")},
            trailingIcon = {
                Icon(
                    imageVector = icon,
                    "contentDescription",
                    modifier = Modifier.clickable { mExpanded.value = !mExpanded.value })
            }
        )

        DropdownMenu(
            expanded = mExpanded.value,
            onDismissRequest = { mExpanded.value = false },
            modifier = Modifier.fillMaxWidth()
        ) {
            mCities.forEach { label ->
                DropdownMenuItem(
                    text = { CountrySelection(text = label) },
                    onClick = {
                        title.value = label
                        mExpanded.value = false
                    }
                )
            }
        }
    }
}


@Composable
fun CountrySelection(
    text: String
) {
    Box(
        modifier = Modifier
            .padding(top = 4.dp)
            .fillMaxWidth()
            .clip(RoundedCornerShape(5.dp))
            .background(MaterialTheme.colorScheme.onPrimary)
    ) {
        Text(
            text = text,
            style = MaterialTheme.typography.bodyMedium,
            color = MaterialTheme.colorScheme.secondary,
            modifier = Modifier.padding(16.dp)
        )
    }
}