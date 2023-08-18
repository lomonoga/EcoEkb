package com.ecoekb.ui.screens.requestsScreens.requestsList

import androidx.compose.animation.core.Spring
import androidx.compose.animation.core.animateDpAsState
import androidx.compose.animation.core.spring
import androidx.compose.foundation.ExperimentalFoundationApi
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.BoxScope
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.defaultMinSize
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.pager.HorizontalPager
import androidx.compose.foundation.pager.rememberPagerState
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material3.Button
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.clip
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.navigation.NavController
import com.ecoekb.domain.models.RequestStatus
import com.ecoekb.ui.navigation.Screens

sealed class Page {
    object Active : Page()
    object Processing : Page()
}

val pages = listOf(
    Page.Active, Page.Processing
)

@OptIn(ExperimentalFoundationApi::class)
@Composable
fun RequestsList(
    navController: NavController,
    viewModel: RequestsListViewModel = hiltViewModel()
) {

    val statePager = rememberPagerState()

    Column(
        modifier = Modifier
            .background(MaterialTheme.colorScheme.background)
            .fillMaxSize()
    ) {
        Box {
            Indicators(size = 1, index = statePager.currentPage)
        }

        HorizontalPager(
            state = statePager,
            pageCount = pages.size
        ) { pageIndex ->
            when (pages[pageIndex]) {
                Page.Active -> ActivePage(navController, viewModel)
                Page.Processing -> ProfilePage(navController, viewModel)
            }
        }
    }
}

@Composable
fun BoxScope.Indicators(size: Int, index: Int) {
    Row(
        modifier = Modifier
            .padding(
                start = 16.dp,
                end = 16.dp,
                top = 16.dp
            )
            .fillMaxWidth()
            .clip(RoundedCornerShape(5.dp))
            .background(MaterialTheme.colorScheme.onPrimary)
    ) {
        Indicator(
            textOnButton = "Активные",
            isSelected = 0 == index,
            modifier = Modifier
                .fillMaxWidth()
                .weight(1f)
        )
        Indicator(
            textOnButton = "Завершенные",
            isSelected = 1 == index,
            modifier = Modifier
                .fillMaxWidth()
                .weight(1f)
        )
    }
}

@Composable
fun Indicator(
    textOnButton: String,
    isSelected: Boolean,
    modifier: Modifier = Modifier
) {
    val width = animateDpAsState(
        targetValue = if (isSelected) 25.dp else 10.dp,
        animationSpec = spring(dampingRatio = Spring.DampingRatioMediumBouncy),
        label = ""
    )

    var selectedButtonColor = MaterialTheme.colorScheme.onPrimary
    var selectedTextColor = MaterialTheme.colorScheme.onBackground

    if (isSelected) {
        selectedButtonColor = MaterialTheme.colorScheme.primary
        selectedTextColor = MaterialTheme.colorScheme.onPrimary
    }

    Box(
        contentAlignment = Alignment.Center,
        modifier = modifier
            .padding(10.dp)
            .clip(RoundedCornerShape(5.dp))
            .background(selectedButtonColor)
    ) {
        Text(
            text = textOnButton,
            style = MaterialTheme.typography.bodyMedium,
            color = selectedTextColor,
            modifier = Modifier.padding(10.dp)
        )
    }
}

@Composable
fun ActivePage(
    navController: NavController,
    viewModel: RequestsListViewModel = hiltViewModel()
){
    Column(
        modifier = Modifier
            .fillMaxSize()
            .padding(
                start = 16.dp,
                end = 16.dp,
            ),
        verticalArrangement = Arrangement.Bottom
    ) {
        LazyColumn(
            modifier = Modifier.weight(1f),
        ) {
            viewModel.filterRequestByStatus(RequestStatus().ACTIVE).forEach {
                item {
                    RequestCard(
                        navController = navController,
                        request = it
                    )
                }
            }
        }
        Button(
            onClick = { navController.navigate(Screens.RequestsCreate.route) },
            modifier = Modifier
                .fillMaxWidth()
                .padding(
                    bottom = 16.dp
                )
                .defaultMinSize(minHeight = 60.dp),
            shape = RoundedCornerShape(5.dp)
        ) {
            Text("Создать обращение")
        }
    }

}

@Composable
fun ProfilePage(
    navController: NavController,
    viewModel: RequestsListViewModel = hiltViewModel()
){
    Column(
        modifier = Modifier
            .fillMaxSize()
            .padding(
                start = 16.dp,
                end = 16.dp,
            ),
        verticalArrangement = Arrangement.Bottom
    ) {
        LazyColumn(
            modifier = Modifier.weight(1f),
        ) {
            viewModel.filterRequestByStatus(RequestStatus().COMPLETE).forEach {
                item {
                    RequestCard(
                        navController = navController,
                        request = it
                    )
                }
            }
        }
        Button(
            onClick = { navController.navigate(Screens.RequestsCreate.route) },
            modifier = Modifier
                .fillMaxWidth()
                .padding(
                    bottom = 16.dp
                )
                .defaultMinSize(minHeight = 60.dp),
            shape = RoundedCornerShape(5.dp)
        ) {
            Text("Создать обращение")
        }
    }
}

