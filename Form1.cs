using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BISQMH
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            // Способы сортировки числовых массивов - алгоритмы, их программная реализация и наглядный сравнительный анализ.
            InitializeComponent();
        }

        private List<Guna2GradientPanel> panels;
        private int[] values;
        private Random random = new Random();
        private int animationDelay = 200;
        private bool isSorting = false;

        private void FormMenu_Load(object sender, EventArgs e)
        {
            panels = new List<Guna2GradientPanel>
            {
                guna2GradientPanel1, guna2GradientPanel2, guna2GradientPanel3,
                guna2GradientPanel4, guna2GradientPanel5, guna2GradientPanel6,
                guna2GradientPanel7, guna2GradientPanel8, guna2GradientPanel9,
                guna2GradientPanel10, guna2GradientPanel11, guna2GradientPanel12,
                guna2GradientPanel13, guna2GradientPanel14, guna2GradientPanel15,
                guna2GradientPanel16, guna2GradientPanel17
            };
            GenerateRandomValues();
            UpdatePanelHeights();
        }

        private void GenerateRandomValues()
        {
            values = new int[17];
            for (int i = 0; i < 17; i++)
            {
                values[i] = random.Next(20, 200);
            }
        }

        private void UpdatePanelHeights()
        {
            for (int i = 0; i < panels.Count; i++)
            {
                panels[i].Height = values[i];
                panels[i].Top = 229 - values[i];
            }
        }

        private void SetButtonsEnabled(bool enabled)
        {
            guna2ButtonBubbleSort.Enabled = enabled;
            guna2Button2InsertionSort.Enabled = enabled;
            guna2ButtonSelectionSort.Enabled = enabled;
            guna2ButtonQuickSort.Enabled = enabled;
            guna2ButtonMergeSort.Enabled = enabled;
            guna2ButtonHeapSort.Enabled = enabled;
            guna2ButtonRnd.Enabled = enabled;
        }

        // BUBBLE SORT
        private async void guna2ButtonBubbleSort_Click(object sender, EventArgs e)
        {
            if (isSorting) return;
            isSorting = true;
            SetButtonsEnabled(false);
            await BubbleSortVisualization();
            SetButtonsEnabled(true);
            isSorting = false;
        }

        private async Task BubbleSortVisualization()
        {
            int n = values.Length;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    panels[j].FillColor = Color.Red;
                    panels[j + 1].FillColor = Color.Red;

                    await Task.Delay(animationDelay);

                    if (values[j] > values[j + 1])
                    {
                        int temp = values[j];
                        values[j] = values[j + 1];
                        values[j + 1] = temp;
                        await AnimateSwap(j, j + 1);
                    }

                    panels[j].FillColor = Color.FromArgb(94, 148, 255);
                    panels[j + 1].FillColor = Color.FromArgb(94, 148, 255);
                }
                panels[n - i - 1].FillColor = Color.LightGreen;
            }
            panels[0].FillColor = Color.LightGreen;
        }

        // INSERTION SORT
        private async void guna2Button2InsertionSort_Click(object sender, EventArgs e)
        {
            if (isSorting) return;
            isSorting = true;
            SetButtonsEnabled(false);
            await InsertionSortVisualization();
            SetButtonsEnabled(true);
            isSorting = false;
        }

        private async Task InsertionSortVisualization()
        {
            int n = values.Length;

            for (int i = 1; i < n; i++)
            {
                int key = values[i];
                int j = i - 1;

                panels[i].FillColor = Color.Yellow;
                await Task.Delay(animationDelay);

                while (j >= 0 && values[j] > key)
                {
                    panels[j].FillColor = Color.Red;
                    panels[j + 1].FillColor = Color.Red;
                    await Task.Delay(animationDelay);

                    values[j + 1] = values[j];
                    await AnimateSwap(j, j + 1);

                    panels[j + 1].FillColor = Color.LightGreen;
                    j--;
                }

                values[j + 1] = key;
                panels[j + 1].FillColor = Color.LightGreen;

                for (int k = 0; k <= i; k++)
                {
                    panels[k].FillColor = Color.LightGreen;
                }
            }
        }

        // SELECTION SORT
        private async void guna2ButtonSelectionSort_Click(object sender, EventArgs e)
        {
            if (isSorting) return;
            isSorting = true;
            SetButtonsEnabled(false);
            await SelectionSortVisualization();
            SetButtonsEnabled(true);
            isSorting = false;
        }

        private async Task SelectionSortVisualization()
        {
            int n = values.Length;

            for (int i = 0; i < n - 1; i++)
            {
                int minIdx = i;
                panels[i].FillColor = Color.Yellow;

                for (int j = i + 1; j < n; j++)
                {
                    panels[j].FillColor = Color.Orange;
                    await Task.Delay(animationDelay / 2);

                    if (values[j] < values[minIdx])
                    {
                        if (minIdx != i)
                            panels[minIdx].FillColor = Color.FromArgb(94, 148, 255);
                        minIdx = j;
                        panels[minIdx].FillColor = Color.Red;
                    }
                    else
                    {
                        panels[j].FillColor = Color.FromArgb(94, 148, 255);
                    }
                }

                if (minIdx != i)
                {
                    int temp = values[minIdx];
                    values[minIdx] = values[i];
                    values[i] = temp;
                    await AnimateSwap(i, minIdx);
                }

                panels[i].FillColor = Color.LightGreen;
                if (minIdx != i)
                    panels[minIdx].FillColor = Color.FromArgb(94, 148, 255);
            }
            panels[n - 1].FillColor = Color.LightGreen;
        }

        // QUICK SORT
        private async void guna2ButtonQuickSort_Click(object sender, EventArgs e)
        {
            if (isSorting) return;
            isSorting = true;
            SetButtonsEnabled(false);
            await QuickSortVisualization(0, values.Length - 1);
            for (int i = 0; i < panels.Count; i++)
            {
                panels[i].FillColor = Color.LightGreen;
            }
            SetButtonsEnabled(true);
            isSorting = false;
        }

        private async Task QuickSortVisualization(int low, int high)
        {
            if (low < high)
            {
                int pi = await Partition(low, high);
                await QuickSortVisualization(low, pi - 1);
                await QuickSortVisualization(pi + 1, high);
            }
        }

        private async Task<int> Partition(int low, int high)
        {
            int pivot = values[high];
            panels[high].FillColor = Color.Yellow;
            int i = (low - 1);

            for (int j = low; j < high; j++)
            {
                panels[j].FillColor = Color.Orange;
                await Task.Delay(animationDelay);

                if (values[j] < pivot)
                {
                    i++;
                    if (i != j)
                    {
                        int temp = values[i];
                        values[i] = values[j];
                        values[j] = temp;
                        await AnimateSwap(i, j);
                    }
                }
                panels[j].FillColor = Color.FromArgb(94, 148, 255);
            }

            int temp2 = values[i + 1];
            values[i + 1] = values[high];
            values[high] = temp2;

            if (i + 1 != high)
                await AnimateSwap(i + 1, high);

            panels[i + 1].FillColor = Color.LightGreen;
            return i + 1;
        }

        // MERGE SORT
        private async void guna2ButtonMergeSort_Click(object sender, EventArgs e)
        {
            if (isSorting) return;
            isSorting = true;
            SetButtonsEnabled(false);
            await MergeSortVisualization(0, values.Length - 1);
            for (int i = 0; i < panels.Count; i++)
            {
                panels[i].FillColor = Color.LightGreen;
            }
            SetButtonsEnabled(true);
            isSorting = false;
        }

        private async Task MergeSortVisualization(int left, int right)
        {
            if (left < right)
            {
                int middle = left + (right - left) / 2;

                await MergeSortVisualization(left, middle);
                await MergeSortVisualization(middle + 1, right);
                await Merge(left, middle, right);
            }
        }

        private async Task Merge(int left, int middle, int right)
        {
            int n1 = middle - left + 1;
            int n2 = right - middle;

            int[] leftArray = new int[n1];
            int[] rightArray = new int[n2];

            for (int i = 0; i < n1; i++)
            {
                leftArray[i] = values[left + i];
                panels[left + i].FillColor = Color.Orange;
            }

            for (int j = 0; j < n2; j++)
            {
                rightArray[j] = values[middle + 1 + j];
                panels[middle + 1 + j].FillColor = Color.Yellow;
            }

            await Task.Delay(animationDelay);

            int iIdx = 0, jIdx = 0;
            int k = left;

            while (iIdx < n1 && jIdx < n2)
            {
                if (leftArray[iIdx] <= rightArray[jIdx])
                {
                    values[k] = leftArray[iIdx];
                    iIdx++;
                }
                else
                {
                    values[k] = rightArray[jIdx];
                    jIdx++;
                }
                UpdatePanelHeights();
                panels[k].FillColor = Color.LightGreen;
                k++;
                await Task.Delay(animationDelay);
            }

            while (iIdx < n1)
            {
                values[k] = leftArray[iIdx];
                UpdatePanelHeights();
                panels[k].FillColor = Color.LightGreen;
                iIdx++;
                k++;
                await Task.Delay(animationDelay);
            }

            while (jIdx < n2)
            {
                values[k] = rightArray[jIdx];
                UpdatePanelHeights();
                panels[k].FillColor = Color.LightGreen;
                jIdx++;
                k++;
                await Task.Delay(animationDelay);
            }
        }

        // HEAP SORT
        private async void guna2ButtonHeapSort_Click(object sender, EventArgs e)
        {
            if (isSorting) return;
            isSorting = true;
            SetButtonsEnabled(false);
            await HeapSortVisualization();
            SetButtonsEnabled(true);
            isSorting = false;
        }

        private async Task HeapSortVisualization()
        {
            int n = values.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
            {
                await Heapify(n, i);
            }

            for (int i = n - 1; i > 0; i--)
            {
                panels[0].FillColor = Color.Red;
                panels[i].FillColor = Color.Red;
                await Task.Delay(animationDelay);

                int temp = values[0];
                values[0] = values[i];
                values[i] = temp;
                await AnimateSwap(0, i);

                panels[i].FillColor = Color.LightGreen;
                await Heapify(i, 0);
            }
            panels[0].FillColor = Color.LightGreen;
        }

        private async Task Heapify(int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n)
            {
                panels[left].FillColor = Color.Orange;
                await Task.Delay(animationDelay / 2);
            }

            if (left < n && values[left] > values[largest])
                largest = left;

            if (right < n)
            {
                panels[right].FillColor = Color.Orange;
                await Task.Delay(animationDelay / 2);
            }

            if (right < n && values[right] > values[largest])
                largest = right;

            if (largest != i)
            {
                int swap = values[i];
                values[i] = values[largest];
                values[largest] = swap;
                await AnimateSwap(i, largest);

                if (left < n) panels[left].FillColor = Color.FromArgb(94, 148, 255);
                if (right < n) panels[right].FillColor = Color.FromArgb(94, 148, 255);

                await Heapify(n, largest);
            }
            else
            {
                if (left < n) panels[left].FillColor = Color.FromArgb(94, 148, 255);
                if (right < n) panels[right].FillColor = Color.FromArgb(94, 148, 255);
            }
        }

        private async Task AnimateSwap(int index1, int index2)
        {
            int pos1 = panels[index1].Left;
            int pos2 = panels[index2].Left;
            int steps = 10;
            int distance = pos2 - pos1;

            for (int i = 1; i <= steps; i++)
            {
                panels[index1].Left = pos1 + (distance * i / steps);
                panels[index2].Left = pos2 - (distance * i / steps);
                await Task.Delay(30);
            }
            var temp = panels[index1];
            panels[index1] = panels[index2];
            panels[index2] = temp;
        }

        private void guna2ButtonRnd_Click(object sender, EventArgs e)
        {
            if (isSorting) return;
            GenerateRandomValues();
            UpdatePanelHeights();
            foreach (var panel in panels)
            {
                panel.FillColor = Color.FromArgb(94, 148, 255);
            }
        }

    }
}
